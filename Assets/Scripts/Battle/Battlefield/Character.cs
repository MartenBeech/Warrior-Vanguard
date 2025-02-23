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
        if (gridManager == null)
        {
            Debug.LogError("❌ GridManager is NULL in MoveRight()!");
            return;
        }

        int maxX = (gridManager.columns - 1) * 100;
        Vector2 newPosition = new Vector2(gridPosition.x + 100, gridPosition.y);

        if (gridPosition.x >= maxX)
        {
            Debug.Log($"🚧 {gameObject.name} is at the rightmost cell ({gridPosition.x}), staying still.");
            return;
        }

        if (gridManager.IsCellOccupied(newPosition))
        {
            Debug.Log($"🚧 {gameObject.name} is blocked at {gridPosition}, staying still.");
            return;
        }

        gridPosition = newPosition;
        transform.position = new Vector2(gridPosition.x, gridPosition.y);
    }

    public void MoveLeft()
    {
        if (gridManager == null)
        {
            Debug.LogError("❌ GridManager is NULL in MoveLeft()!");
            return;
        }

        int minX = 0; // Leftmost column
        Vector2 newPosition = new Vector2(gridPosition.x - 100, gridPosition.y);

        if (gridPosition.x <= minX)
        {
            Debug.Log($"🚧 {gameObject.name} is at the leftmost cell ({gridPosition.x}), staying still.");
            return;
        }

        if (gridManager.IsCellOccupied(newPosition))
        {
            Debug.Log($"🚧 {gameObject.name} is blocked at {gridPosition}, staying still.");
            return;
        }

        gridPosition = newPosition;
        transform.position = new Vector2(gridPosition.x, gridPosition.y);
    }
}
