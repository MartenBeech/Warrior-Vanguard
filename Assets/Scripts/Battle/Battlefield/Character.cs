using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Vector2 gridPosition;
    private GridManager gridManager;
    private CardStats cardStats;
    private HoverWarrior hoverWarrior;
    public enum Direction {
        Left, Right
    };
    private CharacterSpawner.Alignment alignment;

    public GameObject attackText;
    public GameObject healthText;
    public GameObject image;

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

    public void SetGridManager(GridManager gridManager) {
        this.gridManager = gridManager;
    }

    public void SetPosition(Vector2 position) {
        gridPosition = position;
        transform.position = position;
    }

    public void MoveWarrior(Direction direction) {
        bool isEndOfBoard = false;
        Vector2 newPosition = GetFrontCellPosition(gridPosition, direction);

        if (direction == Direction.Left) {
            float endOfBoardPosition = gridManager.getLeftMostGridPositionX();
            isEndOfBoard = gridPosition.x <= endOfBoardPosition;
        } else if (direction == Direction.Right) {
            float endOfBoardPosition = gridManager.getRightMostGridPositionX();
            isEndOfBoard = gridPosition.x >= endOfBoardPosition;
        }

        Character frontCellCharacter = gridManager.GetCellCharacter(newPosition);

        // If no character in front, move
        if (!frontCellCharacter) {
            gridPosition = newPosition;
            transform.position = new Vector2(gridPosition.x, gridPosition.y);
            return;
        }

        // If character in front is a friend or end of the board, do nothing
        if (isEndOfBoard || frontCellCharacter.alignment == alignment) return;

        // If character in front is an enemy, attack
        if (frontCellCharacter.alignment != alignment) {
            AttackCharacter(cardStats.attack, frontCellCharacter);
        }
    }

    private Vector2 GetFrontCellPosition(Vector2 currentPosition, Direction direction) {
        float gridSpacingX = gridManager.GetGridSpacingX();

        if (direction == Direction.Left) {
            return new(currentPosition.x - gridSpacingX, currentPosition.y);
        } else if (direction == Direction.Right) {
            return new(currentPosition.x + gridSpacingX, currentPosition.y);
        }
        return new(currentPosition.x, currentPosition.y);
    }

    private void AttackCharacter(int damage, Character character) {
        character.cardStats.health -= damage;
        character.UpdateWarriorUI();
        if (character.cardStats.health <= 0) {
            Destroy(character.gameObject);
        }
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
