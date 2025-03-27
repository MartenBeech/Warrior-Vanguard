using System.Collections.Generic;
using System.Threading.Tasks;
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
        float cellSpacing = 150 / columns;

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

    public async Task SelectCell(Vector2 selectedGridIndex) {
        if (GetCellCharacter(selectedGridIndex)) return;

        if (characterSpawner.getIsSpawning(CharacterSpawner.Alignment.Enemy)) {
            WarriorStats luigiStats = new Luigi().GetStats();
            luigiStats.alignment = CharacterSpawner.Alignment.Enemy;

            await characterSpawner.Spawn(selectedGridIndex, luigiStats, EnemySummonerObject.position);
            return;
        }

        if (GameManager.turn == CharacterSpawner.Alignment.Friend) {
            if (friendHand.selectedCard == null) return;
            await friendHand.PlayCardFromHand(characterSpawner, selectedGridIndex);
        } else if (GameManager.turn == CharacterSpawner.Alignment.Enemy) {
            if (enemyHand.selectedCard == null) return;
            await enemyHand.PlayCardFromHand(characterSpawner, selectedGridIndex);
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

    public List<GridCell> GetEmptyDeploys(bool largeDeployArea, CharacterSpawner.Alignment alignment) {
        List<GridCell> cells = new();
        if (alignment == CharacterSpawner.Alignment.Friend) {
            for (int x = 0; x < (largeDeployArea ? Mathf.Floor(columns / 2) : 3); x++) {
                for (int y = 0; y < rows; y++) {
                    if (!GetCellCharacter(new Vector2(x, y))) {
                        cells.Add(grid[x, y]);
                    }
                }
            }
        } else if (alignment == CharacterSpawner.Alignment.Enemy) {
            for (int x = columns - 1; x >= (largeDeployArea ? columns - Mathf.Floor(columns / 2) : columns - 3); x--) {
                for (int y = 0; y < rows; y++) {
                    if (!GetCellCharacter(new Vector2(x, y))) {
                        cells.Add(grid[x, y]);
                    }
                }
            }
        }
        return cells;
    }

    public GridCell GetRandomEmptyDeploy(bool largeDeployArea, CharacterSpawner.Alignment alignment) {
        List<GridCell> cells = GetEmptyDeploys(largeDeployArea, alignment);
        if (cells.Count == 0) return null;

        int randomIndex = Rng.Range(0, cells.Count);
        return cells[randomIndex];
    }

    public void HighlightCell(GridCell cell) {
        cell.GetComponent<Outline>().enabled = true;
        cell.GetComponent<Outline>().effectColor = ColorPalette.GetColor(ColorPalette.ColorEnum.teal);
        cell.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.tealWeak);
    }

    public void ClearHighlightedCell(GridCell cell) {
        cell.GetComponent<Outline>().enabled = false;
        cell.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
    }

    public void HighlightDeploys(bool largeDeployArea, CharacterSpawner.Alignment alignment) {
        List<GridCell> cells = GetEmptyDeploys(largeDeployArea, alignment);
        for (int i = 0; i < cells.Count; i++) {
            HighlightCell(cells[i]);
        }
    }

    public void ClearHighlightedDeploys() {
        for (int x = 0; x < columns; x++) {
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

    public int GetDistanceBetweenCharacters(Character character1, Character character2) {
        if (character1.gridIndex.y != character2.gridIndex.y) return -1;

        float dist = Mathf.Abs(character1.gridIndex.x - character2.gridIndex.x);
        return (int)dist;
    }
}
