using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Vector2 gridPosition;
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
        attackText.text = $"{stats.attack}";
        healthText.text = $"{stats.health}";
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");
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
        gridPosition = position;
        transform.position = position;
    }

    public void SetRemainingActions(int remainingAttacks, int remainingSteps) {
        this.remainingAttacks = remainingAttacks;
        this.remainingSteps = remainingSteps;
    }

    public async Task MoveWarrior(Direction direction) {
        Vector2 newPosition = GetFrontCellPosition(gridPosition, direction);

        if (IsOutOfField(newPosition, direction)) return;

        Character frontCellCharacter = gridManager.GetCellCharacter(newPosition);

        // If no character in front, move
        if (!frontCellCharacter && remainingSteps > 0) {
            ObjectAnimation objectAnimation = GetComponentInChildren<ObjectAnimation>();
            await objectAnimation.MoveObject(transform.position, newPosition);
            gridPosition = newPosition;
            remainingSteps--;
            return;
        }

        // If character in front is a friend try to go past them
        if (frontCellCharacter && frontCellCharacter.alignment == alignment && remainingSteps > 1) {
            for (int i = 2; i <= remainingSteps; i++) {
                Vector2 position = GetFrontCellPosition(gridPosition, direction, i);
                Character characterOnCell = gridManager.GetCellCharacter(position);

                if (!characterOnCell && !IsOutOfField(position, direction)) {
                    gridPosition = position;
                    transform.position = new Vector2(gridPosition.x, gridPosition.y);
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

    public bool IsOutOfField(Vector2 position, Direction direction) {
        return direction == Direction.Left ? position.x < gridManager.getLeftMostGridPositionX() : position.x > gridManager.getRightMostGridPositionX();
    }

    public bool IsOutOfField(Vector2 position) {
        return position.x < gridManager.getLeftMostGridPositionX() || position.x > gridManager.getRightMostGridPositionX();
    }

    public async Task StandAndAttack(Direction direction) {
        remainingAttacks--;
        for (int i = 1; i <= stats.range; i++) {
            Vector2 position = GetFrontCellPosition(gridPosition, direction, i);
            Character characterOnCell = gridManager.GetCellCharacter(position);

            if (characterOnCell && characterOnCell.alignment != alignment) {
                await Attack(this, characterOnCell);
                break;
            }

            if (IsOutOfField(position)) {
                if (alignment == CharacterSpawner.Alignment.Enemy) {
                    Summoner friendSummoner = gameManager.friendSummonerObject.GetComponent<Summoner>();
                    await friendSummoner.Damage(this, stats.attack);
                    break;
                }
                if (alignment == CharacterSpawner.Alignment.Friend) {
                    Summoner enemySummoner = gameManager.enemySummonerObject.GetComponent<Summoner>();
                    await enemySummoner.Damage(this, stats.attack);
                    break;
                }
            }
        }
        await EndTurn(this);
    }

    private async Task Attack(Character dealer, Character target) {
        int damageDealt = await TakeDamage(target, dealer.stats.attack);

        if (damageDealt > 0) {
            stats.ability.bloodlust.Trigger(this);
        }

        dealer.stats.ability.poison.Trigger(target);
    }

    public async Task<int> TakeDamage(Character target, int damage) {
        if (damage > 0) {
            target.stats.health -= damage;
            target.UpdateWarriorUI();
            if (target.stats.health <= 0) {
                Kill(target);
            }
        }

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(target.transform, damage.ToString());

        return damage;
    }

    private void Kill(Character character) {
        gameManager.RemoveCharacter(character);
        gridManager.RemoveCharacter(character);

        character.stats.ability.revive.Trigger(character, gridManager, FindFirstObjectByType<CharacterSpawner>());
        character.stats.ability.hydraSplit.Trigger(character, gridManager, FindFirstObjectByType<CharacterSpawner>());

        Destroy(character.gameObject);
    }

    private async Task EndTurn(Character character) {
        await character.stats.ability.poisoned.Trigger(character);
    }

    private Vector2 GetFrontCellPosition(Vector2 currentPosition, Direction direction, int range = 1) {
        float gridSpacingX = gridManager.GetGridSpacingX();

        if (direction == Direction.Left) {
            return new(currentPosition.x - (gridSpacingX * range), currentPosition.y);
        } else if (direction == Direction.Right) {
            return new(currentPosition.x + (gridSpacingX * range), currentPosition.y);
        }
        return currentPosition;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.ShowCardFromBattlefield(stats, gridPosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.HideCard();
        }
    }
}
