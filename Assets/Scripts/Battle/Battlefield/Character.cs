using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Collections.Generic;

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
    public enum DamageType {
        Physical, Magical
    };

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
            ObjectAnimation objectAnimation = GetComponent<ObjectAnimation>();
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
            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, i);
            Character characterOnCell = gridManager.GetCellCharacter(newGridIndex);

            if (characterOnCell && characterOnCell.alignment != alignment) {
                await Attack(characterOnCell);
                break;
            }

            if (IsOutOfField(newGridIndex)) {
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

        List<Task> asyncFunctions = new() {
            stats.ability.splash.Trigger(this, target, gridManager),
            Strike(target, damage)
        };
        await Task.WhenAll(asyncFunctions);

        if (stats.ability.darkTouch.Trigger(this, target)) {
            await target.Die(this);
        }
        stats.ability.weaken.Trigger(this, target);
        stats.ability.bloodlust.Trigger(this);
        await target.stats.ability.retaliate.Trigger(this, target);
    }

    public async Task Strike(Character target, int damage) {
        stats.ability.poison.Trigger(this, target);
        stats.ability.frozenTouch.Trigger(this, target);

        damage = await target.TakeDamage(this, damage, stats.damageType);

        if (damage > 0) {
            await stats.ability.lifeSteal.Trigger(this, damage);
        }
    }

    public async Task<int> TakeDamage(Character dealer, int damage, DamageType damageType) {
        if (stats.ability.stealth.TriggerTakeDamage(this)) {
            damage = (int)Mathf.Ceil(damage / 2f);
        }
        if (stats.ability.skeletal.Trigger(dealer, this)) {
            damage = (int)Mathf.Ceil(damage / 2f);
        }
        if (damageType == DamageType.Physical) {
            if (stats.ability.incorporeal.Trigger(this)) {
                if (damage > 1) {
                    damage = 1;
                }
            }
        }

        List<Task> asyncFunctions = new();

        if (damage > 0) {
            stats.AddHealth(-damage);
            UpdateWarriorUI();

            if (stats.GetHealth() <= 0) {
                asyncFunctions.Add(Die(dealer));
            }
        }

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        asyncFunctions.Add(floatingText.CreateFloatingText(transform, damage.ToString()));

        await Task.WhenAll(asyncFunctions);

        return damage;
    }

    public async Task Heal(int amount) {
        stats.AddHealth(amount);
        UpdateWarriorUI();

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(transform, amount.ToString(), ColorPalette.ColorEnum.green);
    }

    private async Task Die(Character dealer) {
        gameManager.RemoveCharacter(this);
        gridManager.RemoveCharacter(this);

        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();

        if (dealer != this) {
            dealer.stats.ability.cannibalism.Trigger(dealer);
            dealer.stats.ability.raiseDead.Trigger(dealer, this, characterSpawner);
            List<Character> friends = gridManager.GetFriends(dealer.alignment);
            foreach (Character friend in friends) {
                friend.stats.ability.deathCall.Trigger(friend, this, characterSpawner);
            }
        }

        stats.ability.revive.Trigger(this, characterSpawner);
        stats.ability.hydraSplit.Trigger(this, characterSpawner);

        ObjectAnimation objectAnimation = GetComponent<ObjectAnimation>();
        Hand hand = FindFirstObjectByType<Hand>();
        await stats.ability.afterlife.Trigger(this, objectAnimation, gridManager, hand);

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
