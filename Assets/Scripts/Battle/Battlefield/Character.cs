using UnityEngine;

public class Character : MonoBehaviour
{
    public Vector2Int gridPosition; // Character's grid coordinates
    private GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>(); // Find GridManager in the scene
    }


    public void SetPosition(Vector2Int newPos)
    {
        gridPosition = newPos;
        transform.position = new Vector2(newPos.x, newPos.y);
    }

    public void MoveRight()
    {
        if (gridManager == null)
        {
            Debug.LogError("GridManager is not assigned!");
            return;
        }

        int maxX = (gridManager.columns - 1) * 100; // Get max width of the grid

        Vector2Int newPosition = new Vector2Int(gridPosition.x + 100, gridPosition.y);

        // Check if we are at the rightmost cell
        if (gridPosition.x >= maxX)
        {
            Debug.Log($"🚧 {gameObject.name} is at the rightmost cell ({gridPosition.x}), staying still.");
            return;
        }

        // Check if another character is in the new position
        if (gridManager.IsCellOccupied(newPosition))
        {
            Debug.Log($"🚧 {gameObject.name} is blocked at {gridPosition}, staying still.");
            return;
        }

        // Move the character
        gridPosition = newPosition;
        transform.position = new Vector2(gridPosition.x, gridPosition.y);
    }
}
