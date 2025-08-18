using System;
using System.Collections.Generic;
using System.Linq;
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
        GenerateGrid(FriendlySummoner.summonerData.genre);
        ClearHighlightedCells();
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

    void GenerateGrid(Character.Genre genre) {
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

                if (genre != Character.Genre.None) {
                    int randomCell = Rng.Range(0, 12);
                    gridCell.image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cells/{genre}/{randomCell}");
                }

                gridCell.Setup(this, new Vector2(x, y));
                grid[x, y] = gridCell;
            }
        }
    }

    public async Task<bool> SelectCell(Vector2 selectedGridIndex) {
        if (!grid[(int)selectedGridIndex.x, (int)selectedGridIndex.y].IsHighlighed()) return false;

        if (characterSpawner.getIsSpawning(CharacterSpawner.Alignment.Enemy)) {
            WarriorStats luigiStats = new Luigi().GetStats();
            luigiStats.alignment = CharacterSpawner.Alignment.Enemy;

            await characterSpawner.Spawn(selectedGridIndex, luigiStats, EnemySummonerObject.position);
            return true;
        }

        if (GameManager.turn == CharacterSpawner.Alignment.Friend) {
            if (friendHand.selectedCard == null) return false;
            await friendHand.PlayCardFromHand(characterSpawner, selectedGridIndex);
        } else if (GameManager.turn == CharacterSpawner.Alignment.Enemy) {
            if (enemyHand.selectedCard == null) return false;
            await enemyHand.PlayCardFromHand(characterSpawner, selectedGridIndex);
        }
        return true;
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

        return Rng.Entry(cells);
    }

    public void HighlightDeploys(bool largeDeployArea, CharacterSpawner.Alignment alignment) {
        List<GridCell> cells = GetEmptyDeploys(largeDeployArea, alignment);
        for (int i = 0; i < cells.Count; i++) {
            cells[i].Highlight();
        }
    }

    public void HighlightEnemies(CharacterSpawner.Alignment alignment, bool withSpell = false) {
        List<Character> enemies = GetEnemies(alignment);
        foreach (var enemy in enemies) {
            if (withSpell && enemy.stats.ability.spellImmunity.GetValue(enemy.stats)) continue;

            GridCell cell = grid[(int)enemy.gridIndex.x, (int)enemy.gridIndex.y];
            cell.Highlight();
        }
    }

    public void HighlightFriends(CharacterSpawner.Alignment alignment, bool withSpell = false) {
        List<Character> friends = GetFriends(alignment);
        foreach (var friend in friends) {
            if (withSpell && friend.stats.ability.spellImmunity.GetValue(friend.stats)) continue;

            GridCell cell = grid[(int)friend.gridIndex.x, (int)friend.gridIndex.y];
            cell.Highlight();
        }
    }

    public void HighlightWarriors(bool withSpell = false) {
        List<Character> warriors = GetCharacters();
        foreach (var warrior in warriors) {
            if (withSpell && warrior.stats.ability.spellImmunity.GetValue(warrior.stats)) continue;

            GridCell cell = grid[(int)warrior.gridIndex.x, (int)warrior.gridIndex.y];
            cell.Highlight();
        }
    }

    public void HighlightAllCells() {
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                grid[x, y].Highlight();
            }
        }
    }

    public void ClearHighlightedCells() {
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                grid[x, y].ClearHighlight();
            }
        }
    }

    public List<GridCell> GetHighlighedCells() {
        List<GridCell> cells = new();
        for (int x = 0; x < columns; x++) {
            for (int y = 0; y < rows; y++) {
                if (grid[x, y].IsHighlighed()) {
                    cells.Add(grid[x, y]);
                }
            }
        }
        return cells;
    }

    public GridCell GetRandomHighlighedCell() {
        List<GridCell> cells = GetHighlighedCells();
        if (cells.Count == 0) return null;

        return Rng.Entry(cells);
    }

    public List<Character> GetNearbyWarriors(Vector2 gridIndex) {
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

    public List<Character> GetNearbyFriends(Character warrior) {
        List<Character> nearbyWarriors = GetNearbyWarriors(warrior.gridIndex);
        List<Character> nearbyFriends = nearbyWarriors.Where(a => a.stats.alignment == warrior.stats.alignment).ToList();

        return nearbyFriends;
    }

    public List<Character> GetNearbyEnemies(Character warrior) {
        List<Character> nearbyWarriors = GetNearbyWarriors(warrior.gridIndex);
        List<Character> nearbyFriends = nearbyWarriors.Where(a => a.stats.alignment != warrior.stats.alignment).ToList();

        return nearbyFriends;
    }

    public List<Character> GetCharacters() {
        List<Character> characters = new();
        foreach (var character in allCharacters) {
            characters.Add(character);
        }
        return characters;
    }

    public List<Character> GetFriends(CharacterSpawner.Alignment alignment) {
        List<Character> friends = new();
        foreach (Character character in allCharacters) {
            if (character.stats.alignment == alignment) {
                friends.Add(character);
            }
        }
        return friends;
    }

    public List<Character> GetEnemies(CharacterSpawner.Alignment alignment) {
        List<Character> enemies = new();
        foreach (Character character in allCharacters) {
            if (character.stats.alignment != alignment) {
                enemies.Add(character);
            }
        }
        return enemies;
    }

    public List<Character> GetDamagedFriends(CharacterSpawner.Alignment alignment) {
        List<Character> friends = GetFriends(alignment);
        List<Character> damagedfriends = friends.Where(friend => friend.stats.GetHealth() < friend.stats.GetHealthMax()).ToList();
        return damagedfriends;
    }

    public int GetDistanceBetweenCharacters(Character character1, Character character2) {
        if (character1.gridIndex.y != character2.gridIndex.y) return -1;

        float dist = Mathf.Abs(character1.gridIndex.x - character2.gridIndex.x);
        return (int)dist;
    }

    public Character GetCharacterBehindTarget(Character target) {
        Character character = null;
        if (target.stats.alignment == CharacterSpawner.Alignment.Enemy) {
            character = GetCellCharacter(target.gridIndex + new Vector2(1, 0));
        } else if (target.stats.alignment == CharacterSpawner.Alignment.Friend) {
            character = GetCellCharacter(target.gridIndex - new Vector2(1, 0));
        }

        return character;
    }

    public List<Character> GetEnemiesInRange(Vector2 gridIndex) {
        Character dealer = GetCellCharacter(gridIndex);
        List<Character> enemiesInRange = new();
        int xIncrement = dealer.stats.alignment == CharacterSpawner.Alignment.Friend
            ? 1
            : dealer.stats.alignment == CharacterSpawner.Alignment.Enemy
            ? -1
            : 0;

        for (int i = 1; i <= dealer.stats.range; i++) {
            Character character = GetCellCharacter(new Vector2(gridIndex.x + (xIncrement * i), gridIndex.y));
            if (character && character.stats.alignment != dealer.stats.alignment) {
                enemiesInRange.Add(character);
            }
        }

        return enemiesInRange;
    }

    public Character GetRandomEnemy(Character dealer) {
        List<Character> enemies = GetEnemies(dealer.stats.alignment);

        Character enemy = Rng.Entry(enemies);
        return enemy;
    }

    public List<Character> GetFriendsOnColumn(Character warrior) {
        List<Character> friends = new();

        for (int y = 0; y <= rows; y++) {
            if (y == warrior.gridIndex.y) continue;

            Character character = GetCellCharacter(new Vector2(warrior.gridIndex.x, y));
            if (character != null && character.stats.alignment == warrior.stats.alignment) {
                friends.Add(character);
            }
        }

        return friends;
    }
}
