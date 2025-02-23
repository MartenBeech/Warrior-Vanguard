using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2 gridPosition;
    private GridManager gridManager;

    public void SetGridManager(GridManager gm)
    {
        gridManager = gm;
    }

    public void SetPosition(Vector2 newPos)
    {
        gridPosition = newPos;
        transform.position = new Vector2(newPos.x, newPos.y);
    }

    public void MoveRight()
    {
        int rightMostColumn = (gridManager.columns - 1) * 100;
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
