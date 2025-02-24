using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject friendPrefab;
    public GameObject enemyPrefab;
    private bool spawningFriend = false;
    private bool spawningEnemy = false;
    public GridManager gridManager;
    public GameManager gameManager;

    public void ActivateSpawnFriend()
    {
        spawningFriend = true;
    }

    public void ActivateSpawnEnemy()
    {
        spawningEnemy = true;
    }

    public bool getSpawningEnemy()
    {
        return spawningEnemy;
    }

    public void SpawnCharacter(Vector2 cell)
    {
        if (spawningFriend)
        {
            spawningFriend = false;
            Spawn(cell, friendPrefab, false);
        }

        if (spawningEnemy)
        {
            spawningEnemy = false;
            Spawn(cell, enemyPrefab, true);
        }
    }

    private void Spawn(Vector2 cell, GameObject prefab, bool isEnemy)
    {
        if (gridManager.IsCellOccupied(cell))
        {
            return;
        }

        Vector2 spawnPosition = new Vector2(cell.x, cell.y);
        GameObject newUnit = Instantiate(prefab, spawnPosition, Quaternion.identity);
        Character characterScript = newUnit.GetComponent<Character>();

        if (characterScript != null)
        {
            characterScript.SetPosition(cell);
            characterScript.SetGridManager(gridManager);
            gameManager.RegisterCharacter(characterScript, isEnemy);
        }
    }

}
