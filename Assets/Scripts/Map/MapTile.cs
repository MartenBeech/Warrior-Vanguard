using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour {
    public bool isUnlocked = false;
    public bool isCompleted = false;
    public MapTile[] nextTiles;
    public Button tileButton;
    public GameObject checkmark;
    public TileType tileType;
    public EnemyType enemyType;

    public enum TileType {
        Battlefield,
        Shop,

        Event
    }

    public enum EnemyType {
        Normal,
        Miniboss,
        Boss
    }

    private void Start() {
        tileButton = GetComponent<Button>();
        UpdateTileAccess();
        tileButton.onClick.AddListener(TileClicked);
    }

    public void SetUnlocked(bool state) {
        isUnlocked = state;
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

    public void UnlockNextTiles() {
        foreach (MapTile tile in nextTiles) {
            tile.isUnlocked = true;
            tile.UpdateTileAccess();
        }
    }

    public void UpdateTileAccess() {
        if (isUnlocked)
            tileButton.interactable = true;
        else
            tileButton.interactable = false;
    }

    private void TileClicked() {
        TileManager tileManager = FindFirstObjectByType<TileManager>();
        tileManager.ClearLastCompleted();
        tileManager.MarkTileAsCompleted(this);
        LevelManager.SetCurrentTile(this);
        PlayerPrefs.SetInt($"RewardChosen", 0);

        switch (tileType) {
            case TileType.Battlefield:
                SceneLoader.LoadBattlefield();
                break;
            case TileType.Shop:
                SceneLoader.LoadShop();
                break;
            case TileType.Event:
                SceneLoader.LoadEvent();
                break;
        }
    }
}
