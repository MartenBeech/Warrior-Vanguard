using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 2;
    public int columns = 8;
    public GameObject cellPrefab;
    public CharacterSpawner characterSpawner;

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
        Vector2Int selectedCell = new Vector2Int(x * 100, y * 100);
        characterSpawner.TrySpawnCharacter(selectedCell);
    }

    public Vector2Int? SelectedCell { get; private set; }
}