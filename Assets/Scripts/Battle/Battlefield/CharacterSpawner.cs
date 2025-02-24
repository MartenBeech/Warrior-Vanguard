using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public enum Alignment
    {
        Null, Enemy, Friend
    };
    public GameObject friendPrefab;
    public GameObject enemyPrefab;
    private Alignment spawningAlignment;
    public GridManager gridManager;
    public GameManager gameManager;

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


        spawningAlignment = Alignment.Null;
        return Spawn(cell, friendPrefab, spawningAlignment);
    }

    private bool Spawn(Vector2 cell, GameObject prefab, Alignment alignment)
    {
        if (gridManager.IsCellOccupied(cell))
        {
            return false;
        }

        Vector2 spawnPosition = new Vector2(cell.x, cell.y);
        GameObject newUnit = Instantiate(prefab, spawnPosition, Quaternion.identity);
        Character characterScript = newUnit.GetComponent<Character>();

        if (characterScript != null)
        {
            characterScript.SetPosition(cell);
            characterScript.SetGridManager(gridManager);
            gameManager.RegisterCharacter(characterScript, alignment);
            return true;
        }

        return false;
    }

}
