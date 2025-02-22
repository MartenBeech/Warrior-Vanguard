using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab; // Assign in Inspector
    public GridManager gridManager;   // Assign in Inspector

    private bool isSpawnMode = false; // Flag to track if we are in spawn mode

    public void ActivateSpawnMode()
    {
        isSpawnMode = true;
        Debug.Log("Spawn mode activated! Click a grid cell to place a character.");
    }

    public void TrySpawnCharacter(Vector2Int cell)
    {
        if (!isSpawnMode) return;

        Vector3 spawnPosition = new Vector3(cell.x, cell.y);
        GameObject newCharacter = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);

        isSpawnMode = false;
    }
}
