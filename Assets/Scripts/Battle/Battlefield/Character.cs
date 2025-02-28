using UnityEngine;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 gridPosition;
    private GridManager gridManager;
    private CardStats cardStats;
    private HoverWarrior hoverWarrior;
    public enum Direction
    {
        Left, Right
    };

    public void SetStats(CardStats cardStats)
    {
        this.cardStats = cardStats;
    }

    public void SetHoverWarrior(HoverWarrior hoverWarrior)
    {
        this.hoverWarrior = hoverWarrior;
    }

    public void SetGridManager(GridManager gridManager)
    {
        this.gridManager = gridManager;
    }

    public void SetPosition(Vector2 position)
    {
        gridPosition = position;
        transform.position = position;
    }

    public void MoveWarrior(Direction direction)
    {
        Vector2 newPosition = new();
        float gridSpacingX = gridManager.GetGridSpacingX();

        if (direction == Direction.Left)
        {
            newPosition = new(gridPosition.x - gridSpacingX, gridPosition.y);
            float leftMostColumnPosition = gridManager.getLeftMostGridPositionX();

            if (gridPosition.x <= leftMostColumnPosition || gridManager.IsCellOccupied(newPosition)) return;
        }
        else if (direction == Direction.Right)
        {
            newPosition = new(gridPosition.x + gridSpacingX, gridPosition.y);
            float rightMostColumnPosition = gridManager.getRightMostGridPositionX();

            if (gridPosition.x >= rightMostColumnPosition || gridManager.IsCellOccupied(newPosition)) return;
        }

        gridPosition = newPosition;
        transform.position = new Vector2(gridPosition.x, gridPosition.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverWarrior != null)
        {
            hoverWarrior.DisplayCardUI(cardStats);
            hoverWarrior.ShowCard(gridPosition);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverWarrior != null)
        {
            hoverWarrior.HideCard();
        }
    }

}
