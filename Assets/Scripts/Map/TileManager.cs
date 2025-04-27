using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public List<MapTile> mapTiles;
    public RectTransform scrollViewPanel;
    public RewardManager rewardManager;
    public GameObject rewardPanel;


    private void Start() {
        UpdateTileAccess();
    }

    private void UpdateTileAccess() {
        LockAllTiles();

        for (int i = 0; i < mapTiles.Count; i++) {
            bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{i}", 0) == 1;
            bool isLastCompleted = PlayerPrefs.GetInt($"LastCompleted_{i}", 0) == 1;

            if (isCompleted) {
                mapTiles[i].MarkAsCompleted();
            } else {
                mapTiles[i].MarkAsIncompleted();
            }

            if (isLastCompleted) {
                mapTiles[i].UnlockNextTiles();

                //TODO: Scroll to finished tile

                if (mapTiles[i].tileType == MapTile.TileType.Battlefield && PlayerPrefs.GetInt($"RewardChosen", 0) == 0) {
                    rewardManager.ShowReward(mapTiles[i].enemyType);
                }
            }
        }
    }

    public void MarkTileAsCompleted(MapTile tile) {
        if (mapTiles.Contains(tile)) {
            int tileIndex = mapTiles.IndexOf(tile);
            PlayerPrefs.SetInt($"TileCompleted_{tileIndex}", 1);
            PlayerPrefs.SetInt($"LastCompleted_{tileIndex}", 1);
            PlayerPrefs.Save();
        }
    }

    public void ClearLastCompleted() {
        for (int i = 0; i < mapTiles.Count; i++) {
            PlayerPrefs.DeleteKey($"LastCompleted_{i}");
        }
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
