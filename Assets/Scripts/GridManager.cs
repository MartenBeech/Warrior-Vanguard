using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 2;
    public int columns = 8;
    public GameObject cellPrefab; // Assign a simple button prefab in the Inspector

    private GridCell[,] grid;

    void Start()
    {
        grid = new GridCell[columns, rows];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(x * 100, y * 100, 0), Quaternion.identity, transform);
                GridCell gridCell = cell.GetComponent<GridCell>();
                gridCell.Setup(x, y, this);
                grid[x, y] = gridCell;
            }
        }
    }

    public void SelectCell(int x, int y)
    {
        SelectedCell = new Vector2Int(x, y);
    }

    public Vector2Int? SelectedCell { get; private set; }
}