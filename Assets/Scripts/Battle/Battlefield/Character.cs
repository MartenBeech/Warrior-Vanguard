using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Vector2 gridIndex;
    private GridManager gridManager;
    public WarriorStats stats;
    private HoverWarrior hoverWarrior;
    public enum Direction {
        Left, Right
    };
    public CharacterSpawner.Alignment alignment;

    public TMP_Text attackText;
    public TMP_Text healthText;
    public GameObject image;
    private GameManager gameManager;
    public int remainingAttacks = 1;
    public int remainingSteps = 0;

    public void Initiate(GameManager gameManager, GridManager gridManager) {
        this.gameManager = gameManager;
        this.gridManager = gridManager;
    }

    public void UpdateWarriorUI() {
        attackText.text = $"{stats.GetStrength()}";
        healthText.text = $"{stats.GetHealth()}";
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");

        ColorPalette colorPalette = new();
        if (stats.ability.stealth.GetValue(stats)) {
            image.GetComponent<Image>().color = colorPalette.AddTransparency(image.GetComponent<Image>().color, 70);
        } else {
            image.GetComponent<Image>().color = colorPalette.AddTransparency(image.GetComponent<Image>().color, 100);
        }
    }

    public void SetStats(WarriorStats warriorStats) {
        stats = warriorStats;
        UpdateWarriorUI();
    }

    public void SetAlignment(CharacterSpawner.Alignment alignment) {
        this.alignment = alignment;
    }

    public void SetHoverWarrior(HoverWarrior hoverWarrior) {
        this.hoverWarrior = hoverWarrior;
    }

    public void SetPosition(Vector2 position) {
        gridIndex = position;
        transform.position = gridManager.GetCellPosition(position);
    }

    public void SetRemainingActions(int remainingAttacks, int remainingSteps) {
        this.remainingAttacks = remainingAttacks;
        this.remainingSteps = remainingSteps;
    }

    public async Task MoveWarrior(Direction direction) {
        Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction);

        if (IsOutOfField(newGridIndex)) return;

        Character frontCellCharacter = gridManager.GetCellCharacter(newGridIndex);

        // If no character in front, move
        if (!frontCellCharacter && remainingSteps > 0) {
            ObjectAnimation objectAnimation = GetComponentInChildren<ObjectAnimation>();
            await objectAnimation.MoveObject(transform.position, gridManager.GetCellPosition(newGridIndex));
            gridIndex = newGridIndex;
            remainingSteps--;
            return;
        }

        // If character in front is a friend try to go past them
        if (frontCellCharacter && frontCellCharacter.alignment == alignment && remainingSteps > 1) {
            for (int i = 2; i <= remainingSteps; i++) {
                Vector2 position = GetFrontCellIndex(gridIndex, direction, i);
                Character characterOnCell = gridManager.GetCellCharacter(position);

                if (!characterOnCell && !IsOutOfField(position)) {
                    gridIndex = position;
                    transform.position = gridManager.GetCellPosition(gridIndex);
                    remainingSteps -= i;
                    return;
                }

                if (characterOnCell && characterOnCell.alignment != alignment) break;
            }
        }

        if (remainingAttacks < 1) return;

        // If character in within range is an enemy, attack
        await StandAndAttack(direction);
    }

    public bool IsOutOfField(Vector2 gridIndex) {
        return gridIndex.x < 0 || gridIndex.x >= gridManager.columns;
    }

    public async Task StandAndAttack(Direction direction) {
        remainingAttacks--;
        for (int i = 1; i <= stats.range; i++) {
            Vector2 position = GetFrontCellIndex(gridIndex, direction, i);
            Character characterOnCell = gridManager.GetCellCharacter(position);

            if (characterOnCell && characterOnCell.alignment != alignment) {
                await Attack(characterOnCell);
                break;
            }

            if (IsOutOfField(position)) {
                if (alignment == CharacterSpawner.Alignment.Enemy) {
                    Summoner friendSummoner = gameManager.friendSummonerObject.GetComponent<Summoner>();
                    await friendSummoner.Damage(this, stats.GetStrength());
                    break;
                }
                if (alignment == CharacterSpawner.Alignment.Friend) {
                    Summoner enemySummoner = gameManager.enemySummonerObject.GetComponent<Summoner>();
                    await enemySummoner.Damage(this, stats.GetStrength());
                    break;
                }
            }
        }
        await EndTurn(this);
    }

    public async Task Attack(Character target) {
        int damage = stats.GetStrength();
        if (stats.ability.stealth.TriggerAttack(this)) {
            damage *= 2;
        }

        stats.ability.splash.Trigger(this, target, gridManager);
        await Strike(target, damage);

        stats.ability.weaken.Trigger(this, target);
        stats.ability.bloodlust.Trigger(this);
        await target.stats.ability.retaliate.Trigger(this, target);
    }

    public async Task Strike(Character target, int damage) {
        stats.ability.poison.Trigger(this, target);
        stats.ability.frozenTouch.Trigger(this, target);

        damage = await target.TakeDamage(this, damage);

        if (damage > 0) {
            await stats.ability.lifeSteal.Trigger(this, damage);
        }
    }

    public async Task<int> TakeDamage(Character dealer, int damage) {
        if (stats.ability.stealth.TriggerTakeDamage(this)) {
            damage = (int)Mathf.Ceil(damage / 2f);
        }

        if (damage > 0) {
            stats.AddHealth(-damage);

            if (stats.GetHealth() <= 0) {
                Die(dealer);
            } else {
                UpdateWarriorUI();
            }
        }

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(transform, damage.ToString());

        return damage;
    }

    public async Task Heal(int amount) {
        stats.AddHealth(amount);
        UpdateWarriorUI();

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(transform, amount.ToString(), ColorPalette.ColorEnum.green);
    }

    private void Die(Character dealer) {
        gameManager.RemoveCharacter(this);
        gridManager.RemoveCharacter(this);

        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();

        if (dealer != this) {
            dealer.stats.ability.cannibalism.Trigger(dealer);
            dealer.stats.ability.raiseDead.Trigger(dealer, this, characterSpawner);
        }

        stats.ability.revive.Trigger(this, characterSpawner);
        stats.ability.hydraSplit.Trigger(this, characterSpawner);


        Destroy(gameObject);
    }

    private async Task EndTurn(Character character) {
        await character.stats.ability.poisoned.Trigger(character);
    }

    private Vector2 GetFrontCellIndex(Vector2 gridIndex, Direction direction, int range = 1) {
        if (direction == Direction.Left) {
            return new(gridIndex.x - (1 * range), gridIndex.y);
        } else if (direction == Direction.Right) {
            return new(gridIndex.x + (1 * range), gridIndex.y);
        }
        return gridIndex;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.ShowCardFromBattlefield(stats, gridManager.GetCellPosition(gridIndex));
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.HideCard();
        }
    }
}
