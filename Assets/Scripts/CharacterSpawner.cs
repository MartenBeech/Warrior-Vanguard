using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject characterPrefab;
    public GridManager gridManager;

    public void SpawnCharacter()
    {
        if (gridManager.SelectedCell.HasValue)
        {
            Vector2Int cell = gridManager.SelectedCell.Value;
            Vector3 spawnPosition = new Vector3(cell.x * 100, cell.y * 100);

            GameObject newCharacter = Instantiate(characterPrefab, spawnPosition, Quaternion.identity);
            newCharacter.GetComponent<Character>().SetPosition(spawnPosition);
        }
        else
        {
            Debug.Log("No cell selected!");
        }
    }
}
