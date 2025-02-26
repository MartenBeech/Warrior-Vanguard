using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public static int rows = 3;
    public static int columns = 10;
    public GameObject cellPrefab;
    public CharacterSpawner characterSpawner;
    private GridCell[,] grid;
    private List<Character> allCharacters = new List<Character>();
    public Vector2Int? SelectedCell { get; private set; }
    private bool cardUsedSuccessfully;

    void Start()
    {
        grid = new GridCell[columns, rows];
        GenerateGrid();
    }

    void GenerateGrid()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        GridLayoutGroup gridLayoutGroup = GetComponent<GridLayoutGroup>();
        float cellSpacing = 250 / columns;

        float cellWidth = (rectTransform.rect.width / columns) - cellSpacing;
        float cellHeight = (rectTransform.rect.height / rows) - cellSpacing;

        float lowestCellDimension = cellWidth < cellHeight ? cellWidth : cellHeight;
        gridLayoutGroup.cellSize = new Vector2(lowestCellDimension, lowestCellDimension);
        gridLayoutGroup.spacing = new Vector2(cellSpacing, cellSpacing);
        gridLayoutGroup.constraintCount = rows;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject cell = Instantiate(
                    cellPrefab,
                    new Vector2(0, 0),
                    Quaternion.identity,
                    transform
                );
                cell.name = $"Cell[{x},{y}]";
                GridCell gridCell = cell.GetComponent<GridCell>();
                gridCell.Setup(this);
                grid[x, y] = gridCell;
            }
        }
    }

    public void SelectCell(Vector2 pos)
    {
        Vector2 selectedCell = pos;
        if (characterSpawner.getIsSpawning(CharacterSpawner.Alignment.Enemy))
        {
            characterSpawner.SpawnCharacter(selectedCell);
            return;
        }
        Hand hand = FindFirstObjectByType<Hand>();
        if (hand == null || hand.selectedCard == null) return;

        cardUsedSuccessfully = characterSpawner.SpawnCharacter(selectedCell);

        if (cardUsedSuccessfully)
        {
            Destroy(hand.selectedCard.gameObject);
            hand.DeselectCard(hand.selectedCard);
        }
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
                return true;
            }
        }
        return false;
    }

    public void HighlightCell(int x, int y)
    {
        grid[x, y].GetComponent<Outline>().enabled = true;
    }

    public void ClearHighlightedCell(int x, int y)
    {
        grid[x, y].GetComponent<Outline>().enabled = false;
    }

    public void HighlightDeployableCells()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                HighlightCell(x, y);
            }
        }
    }

    public void ClearHighlightedDeployableCells()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                ClearHighlightedCell(x, y);
            }
        }
    }
}
