using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 2;
    public int columns = 8;
    public GameObject cellPrefab;
    public CharacterSpawner characterSpawner;
    private GridCell[,] grid;
    private List<Character> allCharacters = new List<Character>();

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
                GameObject cell = Instantiate(
                    cellPrefab,
                    new Vector2(x * 100, y * 100),
                    Quaternion.identity,
                    transform
                );
                GridCell gridCell = cell.GetComponent<GridCell>();
                gridCell.Setup(x, y, this);
                grid[x, y] = gridCell;
            }
        }
    }

    public void SelectCell(int x, int y)
    {
        Vector2 selectedCell = new Vector2(x * 100, y * 100);
        characterSpawner.TrySpawnCharacter(selectedCell);
    }

    public void RegisterCharacter(Character character)
    {
        if (!allCharacters.Contains(character))
        {
            allCharacters.Add(character);
        }
    }

    public bool IsCellOccupied(Vector2 position)
    {
        foreach (Character character in allCharacters)
        {
            if (character.gridPosition == position)
            {
                return true; // Cell is occupied
            }
        }
        return false; // Cell is free
    }

    public Vector2Int? SelectedCell { get; private set; }
}