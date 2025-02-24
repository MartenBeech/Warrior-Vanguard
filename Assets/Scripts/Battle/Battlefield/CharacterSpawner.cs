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
    public Transform gridManagerObject;
    public GameManager gameManager;

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
        if (gridManager.IsCellOccupied(cell))
        {
            return false;
        }

        Vector2 spawnPosition = new Vector2(cell.x, cell.y);
        GameObject newUnit = Instantiate(prefab, spawnPosition, Quaternion.identity, gridManagerObject);
        Character characterScript = newUnit.GetComponent<Character>();

        Card card = newUnit.GetComponent<Card>();
        Hand hand = FindFirstObjectByType<Hand>();
        if (alignment == Alignment.Friend)
        {
            card.CopyCardValues(hand.selectedCard);
        }
        else
        {
            card.title = "Luigi";
        }
        card.DisplayCardUi();

        if (!characterScript) return false;

        characterScript.SetPosition(cell);
        characterScript.SetGridManager(gridManager);
        gameManager.RegisterCharacter(characterScript, alignment);
        return true;
    }

}
