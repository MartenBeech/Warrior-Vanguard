using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab; // Player character prefab
    public GameObject enemyPrefab; // Enemy character prefab
    private bool spawningCharacter = false;
    private bool spawningEnemy = false;
    public GridManager gridManager;
    public GameManager gameManager;

    public void ActivateSpawnCharacter()
    {
        spawningCharacter = true;
    }

    public void ActivateSpawnEnemy()
    {
        spawningEnemy = true;
    }

    public void TrySpawnCharacter(Vector2 cell)
    {
        if (spawningCharacter) {
            spawningCharacter = false;
            TrySpawn(cell, characterPrefab, false);
        };
        if (spawningEnemy) {
            spawningEnemy = false;
            TrySpawn(cell, enemyPrefab, true);
        }
    }

    private void TrySpawn(Vector2 cell, GameObject prefab, bool isEnemy)
    {
        if (prefab == null)
        {
            Debug.LogError("❌ Prefab is NOT assigned!");
            return;
        }

        if (gridManager.IsCellOccupied(cell))
        {
            Debug.Log($"🚧 Can't spawn at {cell}, already occupied.");
            return;
        }

        // Convert Vector2Int to Vector2 for 2D game
        Vector2 spawnPosition = new Vector2(cell.x, cell.y);

        // Corrected instantiation (for 2D)
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
