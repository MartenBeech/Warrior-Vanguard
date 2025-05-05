using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public List<MapTile> mapTiles;
    public RectTransform scrollViewPanel;
    public RewardManager rewardManager;
    public GameObject rewardPanel;
    public MapTile currentTile;

    private void Start() {
        UpdateTileAccess();
    }

    private void UpdateTileAccess() {
        LockAllTiles();
        bool isTileActive = PlayerPrefs.HasKey("TileActive");

        for (int i = 0; i < mapTiles.Count; i++) {
            bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{i}", 0) == 1;
            bool isLastCompleted = PlayerPrefs.GetInt($"LastCompleted_{i}", 0) == 1;

            if (isCompleted) {
                mapTiles[i].MarkAsCompleted();
            } else {
                mapTiles[i].MarkAsIncompleted();
            }

            if (!isTileActive && isLastCompleted) {
                mapTiles[i].UnlockNextTiles();

                //TODO: Scroll to finished tile

                if (mapTiles[i].tileType == MapTile.TileType.Battlefield && PlayerPrefs.GetInt($"RewardChosen", 0) == 0) {
                    rewardManager.ShowReward(mapTiles[i].enemyType);
                }
            }
        }

        if (isTileActive) {
            int activeTileIndex = PlayerPrefs.GetInt("TileActive", 0);
            mapTiles[activeTileIndex].SetUnlocked(true);
        }
    }

    public void MarkTileAsCurrent(MapTile tile) {
        currentTile = tile;
        int tileIndex = mapTiles.IndexOf(currentTile);
        TileCompleter.MarkTileAsCompleted(false, tileIndex);
        PlayerPrefs.SetInt($"TileActive", tileIndex);
    }

    private void LockAllTiles() {
        for (int i = 0; i < mapTiles.Count; i++) {
            bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{i}", 0) == 1;

            //Set the first tile default unlocked if it haven't been completed yet
            if (i == 0 && !isCompleted) {
                mapTiles[0].SetUnlocked(true);
            } else {
                mapTiles[i].SetUnlocked(false);
            }
        }
    }
}
