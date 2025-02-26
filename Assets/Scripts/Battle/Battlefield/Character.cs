using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2 gridPosition;
    private GridManager gridManager;

    public void SetGridManager(GridManager gridManager)
    {
        this.gridManager = gridManager;
    }

    public void SetPosition(Vector2 position)
    {
        gridPosition = position;
        transform.position = position;
    }

    public void MoveRight()
    {
        int rightMostColumn = (GridManager.columns - 1) * 100;
        Vector2 newPosition = new Vector2(gridPosition.x + 100, gridPosition.y);

        if (gridPosition.x >= rightMostColumn || gridManager.IsCellOccupied(newPosition))
        {
            return;
        }

        gridPosition = newPosition;
        transform.position = new Vector2(gridPosition.x, gridPosition.y);
    }

    public void MoveLeft()
    {
        int leftMostColumn = 0;
        Vector2 newPosition = new Vector2(gridPosition.x - 100, gridPosition.y);

        if (gridPosition.x <= leftMostColumn || gridManager.IsCellOccupied(newPosition))
        {
            return;
        }

        gridPosition = newPosition;
        transform.position = new Vector2(gridPosition.x, gridPosition.y);
    }
}
