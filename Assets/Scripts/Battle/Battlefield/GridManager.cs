using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour {
    public int rows;
    public int columns;
    public GameObject cellPrefab;
    public CharacterSpawner characterSpawner;
    public Hand friendHand;
    public Hand enemyHand;
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

    public float GetLeftMostGridPositionX() {
        return grid[0, 0].transform.position.x;
    }

    public float GetRightMostGridPositionX() {
        return grid[columns - 1, 0].transform.position.x;
    }

    public Vector2 GetCellDimension() {
        return new Vector2(gridLayoutGroup.cellSize.x, gridLayoutGroup.cellSize.y);
    }

    public Vector2 GetCellPosition(Vector2 gridIndex) {

        GridCell cell = grid[(int)gridIndex.x, (int)gridIndex.y];
        Vector2 pos = cell.transform.position;
        return pos;
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
                gridCell.Setup(this, new Vector2(x, y));
                grid[x, y] = gridCell;
            }
        }
    }

    public void SelectCell(Vector2 selectedGridIndex) {
        if (GetCellCharacter(selectedGridIndex)) return;

        if (characterSpawner.getIsSpawning(CharacterSpawner.Alignment.Enemy)) {
            characterSpawner.Spawn(selectedGridIndex, new Luigi().GetStats(), CharacterSpawner.Alignment.Enemy, EnemySummonerObject.position);
            return;
        }

        if (GameManager.turn == CharacterSpawner.Alignment.Friend) {
            if (friendHand.selectedCard == null) return;
            friendHand.PlayCardFromHand(characterSpawner, selectedGridIndex, CharacterSpawner.Alignment.Friend);
        } else if (GameManager.turn == CharacterSpawner.Alignment.Enemy) {
            if (enemyHand.selectedCard == null) return;
            enemyHand.PlayCardFromHand(characterSpawner, selectedGridIndex, CharacterSpawner.Alignment.Enemy);
        }
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

    public Character GetCellCharacter(Vector2 gridIndex) {
        foreach (Character character in allCharacters) {
            if (character.gridIndex == gridIndex) {
                return character;
            }
        }
        return null;
    }

    public List<GridCell> GetEmptyDeploys(bool largeDeployArea) {
        List<GridCell> cells = new();
        for (int x = 0; x < (largeDeployArea ? Mathf.Floor(columns / 2) : 3); x++) {
            for (int y = 0; y < rows; y++) {
                if (!GetCellCharacter(new Vector2(x, y))) {
                    cells.Add(grid[x, y]);
                }
            }
        }
        return cells;
    }

    public GridCell GetRandomEmptyDeploy(bool largeDeployArea) {
        List<GridCell> cells = GetEmptyDeploys(largeDeployArea);
        if (cells.Count == 0) return null;

        int randomIndex = Rng.Range(0, cells.Count);
        return cells[randomIndex];
    }

    public void HighlightCell(GridCell cell) {
        cell.GetComponent<Outline>().enabled = true;
    }

    public void ClearHighlightedCell(GridCell cell) {
        cell.GetComponent<Outline>().enabled = false;
    }

    public void HighlightDeploys(bool largeDeployArea) {
        List<GridCell> cells = GetEmptyDeploys(largeDeployArea);
        for (int i = 0; i < cells.Count; i++) {
            HighlightCell(cells[i]);
        }
    }

    public void ClearHighlightedDeploys(bool largeDeployArea) {
        for (int x = 0; x < (largeDeployArea ? Mathf.Floor(columns / 2) : 3); x++) {
            for (int y = 0; y < rows; y++) {
                ClearHighlightedCell(grid[x, y]);
            }
        }
    }

    public List<Character> GetWarriorsAroundCell(Vector2 gridIndex) {
        List<Character> warriors = new();
        for (int x = (int)gridIndex.x - 1; x <= (int)gridIndex.x + 1; x++) {
            if (x < 0 || x >= columns) continue;

            for (int y = (int)gridIndex.y - 1; y <= (int)gridIndex.y + 1; y++) {
                if (y < 0 || y >= rows) continue;
                if (gridIndex == new Vector2(x, y)) continue;

                Character character = GetCellCharacter(new Vector2(x, y));
                if (character != null) {
                    warriors.Add(character);
                }
            }
        }
        return warriors;
    }

    public List<Character> GetCharacters() {
        return allCharacters;
    }

    public List<Character> GetFriends(CharacterSpawner.Alignment alignment) {
        List<Character> friends = new();
        foreach (Character character in allCharacters) {
            if (character.alignment == alignment) {
                friends.Add(character);
            }
        }
        return friends;
    }

    public List<Character> GetEnemies(CharacterSpawner.Alignment alignment) {
        List<Character> enemies = new();
        foreach (Character character in allCharacters) {
            if (character.alignment != alignment) {
                enemies.Add(character);
            }
        }
        return enemies;
    }
}
