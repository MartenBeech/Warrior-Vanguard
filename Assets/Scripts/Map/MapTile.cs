using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTile : MonoBehaviour {
    public bool isUnlocked = false;
    public bool isCompleted = false;
    public List<MapTile> nextTiles;
    public Button tileButton;
    public GameObject checkmark;
    public TileType tileType;
    public int level = 1;
    public EnemyType enemyType;
    public Vector2 gridIndex;

    private void Start() {
        tileButton = GetComponent<Button>();
        UpdateTileAccess();
        tileButton.onClick.AddListener(TileClicked);
    }

    public void SetUnlocked(bool state) {
        isUnlocked = state;
        tileButton.interactable = state;
    }

    public void MarkAsCompleted(bool state) {
        isCompleted = state;
        checkmark.SetActive(state);
    }

    public void UnlockNextTiles() {
        foreach (MapTile tile in nextTiles) {
            tile.isUnlocked = true;
            tile.UpdateTileAccess();
        }
    }

    public void UpdateTileAccess() {
        tileButton.interactable = isUnlocked;
    }

    private void TileClicked() {
        TileManager tileManager = FindFirstObjectByType<TileManager>();
        tileManager.MarkTileAsCurrent(this);

        LevelManager.SetCurrentTile(this);
        PlayerPrefs.SetInt($"RewardChosen", 0);

        //TEST - Get summoner with the same image as the tile
        // string enemySummonerName = gameObject.GetComponent<Image>().sprite.name;
        // GameManager.enemySummonerName = enemySummonerName;

        switch (tileType) {
            case TileType.Battlefield:
                GameManager.enemySummonerName = EnemySummoner.GetWorthyEnemySummonerName(level);
                SceneLoader.LoadBattlefield();
                break;
            case TileType.Shop:
                SceneLoader.LoadShop();
                break;
            case TileType.Event:
                SceneLoader.LoadEvent();
                break;
            case TileType.Campfire:
                SceneLoader.LoadCampfire();
                break;
        }
    }
}
