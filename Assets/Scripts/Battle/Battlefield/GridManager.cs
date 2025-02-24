using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{
    public int rows = 2;
    public int columns = 8;
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
