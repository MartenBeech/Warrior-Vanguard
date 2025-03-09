using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {
    public int rows;
    public int columns;
    public GameObject cellPrefab;
    public CharacterSpawner characterSpawner;
    public Hand hand;
    private GridCell[,] grid;
    private List<Character> allCharacters = new();
    public Vector2Int? SelectedCell { get; private set; }
    private GridLayoutGroup gridLayoutGroup;
    public Transform EnemySummonerObject;

    void Start() {
        grid = new GridCell[columns, rows];
        GenerateGrid();
    }

    public float GetGridSpacingX() {
        return gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x;
    }

    public float getLeftMostGridPositionX() {
        return grid[0, 0].transform.position.x;
    }

    public float getRightMostGridPositionX() {
        return grid[columns - 1, 0].transform.position.x;
    }

    public Vector2 getCellDimension() {
        return new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
    }

    void GenerateGrid() {
        RectTransform rectTransform = GetComponent<RectTransform>();
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        float cellSpacing = 250 / columns;

        float cellWidth = (rectTransform.rect.width / columns) - cellSpacing;
        float cellHeight = (rectTransform.rect.height / rows) - cellSpacing;

        float lowestCellDimension = cellWidth < cellHeight ? cellWidth : cellHeight;
        gridLayoutGroup.cellSize = new Vector2(lowestCellDimension, lowestCellDimension);
        gridLayoutGroup.spacing = new Vector2(cellSpacing, cellSpacing);
        gridLayoutGroup.constraintCount = rows;

        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
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

    public void SelectCell(Vector2 selectedCellPos) {
        if (GetCellCharacter(selectedCellPos)) return;

        if (characterSpawner.getIsSpawning(CharacterSpawner.Alignment.Enemy)) {
            characterSpawner.Spawn(selectedCellPos, CardDatabase.Instance.allCards[1], CharacterSpawner.Alignment.Enemy, EnemySummonerObject.position);
            return;
        }
        if (hand == null || hand.selectedCard == null) return;

        characterSpawner.Spawn(selectedCellPos, hand.selectedCard.stats, CharacterSpawner.Alignment.Friend, hand.selectedCard.GetComponent<RectTransform>().position);

        Destroy(hand.selectedCard.gameObject);
        hand.DeselectCard(hand.selectedCard);
    }

    public void RegisterCharacter(Character character) {
        if (!allCharacters.Contains(character)) {
            allCharacters.Add(character);
        }
    }

    public void RemoveCharacter(Character character) {
        if (allCharacters.Contains(character)) {
            allCharacters.Remove(character);
        }
    }

    public Character GetCellCharacter(Vector2 position) {
        foreach (Character character in allCharacters) {
            if (character.gridPosition == position) {
                return character;
            }
        }
        return null;
    }

    public List<GridCell> GetEmptyDeploys() {
        List<GridCell> cells = new();
        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < rows; y++) {
                if (!GetCellCharacter(grid[x, y].transform.position)) {
                    cells.Add(grid[x, y]);
                }
            }
        }
        return cells;
    }

    public GridCell GetRandomEmptyDeploy() {
        List<GridCell> cells = GetEmptyDeploys();
        int randomIndex = Rng.Range(0, cells.Count);
        return cells[randomIndex];
    }

    public void HighlightCell(GridCell cell) {
        cell.GetComponent<Outline>().enabled = true;
    }

    public void ClearHighlightedCell(GridCell cell) {
        cell.GetComponent<Outline>().enabled = false;
    }

    public void HighlightDeploys() {
        List<GridCell> cells = GetEmptyDeploys();
        for (int i = 0; i < cells.Count; i++) {
            HighlightCell(cells[i]);
        }
    }

    public void ClearHighlightedDeploys() {
        for (int x = 0; x < 3; x++) {
            for (int y = 0; y < rows; y++) {
                ClearHighlightedCell(grid[x, y]);
            }
        }
    }
}
