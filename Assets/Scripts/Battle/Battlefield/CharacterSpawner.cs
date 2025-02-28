using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public enum Alignment
    {
        Null, Enemy, Friend
    };
    public GameObject warriorPrefab;
    private Alignment spawningAlignment;
    public GridManager gridManager;
    public Transform warriorsObject;
    public GameManager gameManager;
    public HoverWarrior hoverWarrior;

    public void ActivateSpawnEnemy()
    {
        spawningAlignment = Alignment.Enemy;
    }
    public void ActivateSpawn(Alignment alignment)
    {
        spawningAlignment = alignment;
    }

    public bool getIsSpawning(Alignment alignment)
    {
        return spawningAlignment == alignment;
    }

    public bool SpawnCharacter(Vector2 cell)
    {
        if (spawningAlignment == Alignment.Null) return false;

        bool isSpawing = Spawn(cell, warriorPrefab, spawningAlignment);
        spawningAlignment = Alignment.Null;

        return isSpawing;
    }

    private bool Spawn(Vector2 cell, GameObject prefab, Alignment alignment)
    {
        if (gridManager.GetCellCharacter(cell) != null)
        {
            return false;
        }

        Vector2 spawnPosition = cell;
        GameObject warrior = Instantiate(prefab, spawnPosition, Quaternion.identity, warriorsObject);
        warrior.GetComponent<RectTransform>().localScale = gridManager.getCellDimension() / warrior.GetComponent<RectTransform>().rect.width;
        Character character = warrior.GetComponent<Character>();
        if (!character) return false;

        character.SetAlignment(alignment);

        Hand hand = FindFirstObjectByType<Hand>();
        if (alignment == Alignment.Friend)
        {
            character.SetHoverWarrior(hoverWarrior);
            character.SetStats(hand.selectedCard.stats);
        }
        else
        {
            CardStats stats = CardDatabase.Instance.allCards[1];
            character.SetStats(new CardStats
            {
                attack = stats.attack,
                health = stats.health,
                title = stats.title,
                cost = stats.cost
            });
        }

        character.UpdateWarriorUI();
        character.SetPosition(cell);
        character.SetGridManager(gridManager);
        gameManager.RegisterCharacter(character, alignment);
        return true;
    }

}
