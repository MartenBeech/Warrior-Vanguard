using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Vector2 gridPosition;
    private GridManager gridManager;
    public CardStats cardStats;
    private HoverWarrior hoverWarrior;
    public enum Direction {
        Left, Right
    };
    private CharacterSpawner.Alignment alignment;

    public GameObject attackText;
    public GameObject healthText;
    public GameObject image;
    private GameManager gameManager;
    public int remainingAttacks = 1;
    public int remainingSteps = 0;

    public void Initiate(GameManager gameManager, GridManager gridManager) {
        this.gameManager = gameManager;
        this.gridManager = gridManager;
    }

    public void UpdateWarriorUI() {
        if (attackText) attackText.GetComponent<TMP_Text>().text = $"{cardStats.attack}";
        if (healthText) healthText.GetComponent<TMP_Text>().text = $"{cardStats.health}";
        if (image) image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{cardStats.title}");
    }

    public void SetStats(CardStats cardStats) {
        this.cardStats = cardStats;
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

    public async Task StandAndAttack(Direction direction) {
        remainingAttacks--;
        for (int i = 1; i <= cardStats.range; i++) {
            Vector2 position = GetFrontCellPosition(gridPosition, direction, i);
            Character characterOnCell = gridManager.GetCellCharacter(position);

            if (characterOnCell && characterOnCell.alignment != alignment) {
                AttackCharacter(cardStats.attack, characterOnCell);
                FloatingText floatingText = FindFirstObjectByType<FloatingText>();
                await floatingText.CreateFloatingText(position, cardStats.attack.ToString());
                return;
            }
        }
    }

    private void AttackCharacter(int damage, Character character) {
        character.cardStats.health -= damage;
        character.UpdateWarriorUI();
        if (character.cardStats.health <= 0) {
            KillCharacter(character);
        }
    }

    private void KillCharacter(Character character) {
        gameManager.RemoveCharacter(character);
        gridManager.RemoveCharacter(character);
        Destroy(character.gameObject);
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
        if (hoverWarrior != null) {
            hoverWarrior.DisplayCardUI(cardStats);
            hoverWarrior.ShowCard(gridPosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverWarrior != null) {
            hoverWarrior.HideCard();
        }
    }

}
