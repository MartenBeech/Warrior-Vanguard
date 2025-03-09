using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapTile : MonoBehaviour {
    public bool isUnlocked  = false;
    public bool isCompleted = false;
    public MapTile[] nextTiles;
    public Button tileButton;
    public GameObject checkmark;


    private void Start() {
        tileButton = GetComponent<Button>();
        UpdateTileAccess();
        tileButton.onClick.AddListener(GoToBattlefield);
    }

    public void SetUnlocked(bool state) {
        isUnlocked  = state;
        tileButton.interactable = state;
    }

    public void MarkAsCompleted() {
        isCompleted = true;
        checkmark.SetActive(true);
    }

    public void MarkAsIncompleted() {
        isCompleted = false;
        checkmark.SetActive(false);
    }

    public void GoToBattlefield() {
        if (isUnlocked ) {
            TileManager tileManager = FindFirstObjectByType<TileManager>();
            tileManager.ClearLastCompleted();
            tileManager.MarkTileAsCompleted(this);
            LevelManager.SetCurrentTile(this);
            SceneManager.LoadScene("Battlefield");
        }
    }

    public void UnlockNextTiles()
    {
        foreach (MapTile tile in nextTiles)
        {
            tile.isUnlocked = true;
            tile.UpdateTileAccess();
        }
    }

    public void UpdateTileAccess()
    {
        if (isUnlocked)
            tileButton.interactable = true;
        else
            tileButton.interactable = false;
    }
}
