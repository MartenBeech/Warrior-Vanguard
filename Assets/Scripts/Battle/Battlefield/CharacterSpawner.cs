using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public GridManager gridManager;
    public GameManager gameManager; // Reference to the GameManager
    private bool isSpawnMode = false;

    public void ActivateSpawnMode()
    {
        isSpawnMode = true;
    }

    public void TrySpawnCharacter(Vector2Int cell)
    {
        if (!isSpawnMode) return;

        Vector2 spawnPosition = new Vector2(cell.x, cell.y);
        GameObject newCharacter = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);

        Character characterScript = newCharacter.GetComponent<Character>();
        if (characterScript != null)
        {
            characterScript.SetPosition(cell);
            gameManager.RegisterCharacter(characterScript); // Register character in GameManager
        }

        isSpawnMode = false;
    }
}
