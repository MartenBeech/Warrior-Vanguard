using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public List<MapTile> battleTiles;

    private void Start() {
        //If you want to remove all the checkmarks, the following line will clear the cache
        // PlayerPrefs.DeleteAll();

        UpdateTileAccess();
    }

    private void UpdateTileAccess() {
        LockAllTiles();

        for (int i = 0; i < battleTiles.Count; i++) {
            bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{i}", 0) == 1;
            bool isLastCompleted = PlayerPrefs.GetInt($"LastCompleted_{i}", 0) == 1;

            if (isCompleted) {
                battleTiles[i].MarkAsCompleted();
            } else {
                battleTiles[i].MarkAsIncompleted();
            }

            if (isLastCompleted) {
                battleTiles[i].UnlockNextTiles();
            }
        }
    }

    public void MarkTileAsCompleted(MapTile tile) {
        if (battleTiles.Contains(tile)) {
            int tileIndex = battleTiles.IndexOf(tile);
            PlayerPrefs.SetInt($"TileCompleted_{tileIndex}", 1);
            PlayerPrefs.SetInt($"LastCompleted_{tileIndex}", 1);
            PlayerPrefs.Save();
        }
    }

    public void ClearLastCompleted() {
        for (int i = 0; i < battleTiles.Count; i++) {
            PlayerPrefs.DeleteKey($"LastCompleted_{i}");
        }
    }

    private void LockAllTiles() {
        for (int i = 0; i < battleTiles.Count; i++) {
            bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{i}", 0) == 1;
            
            //Set the first tile default unlocked if it haven't been completed yet
            if (i == 0 && !isCompleted) {
                battleTiles[0].SetUnlocked(true);
            } else {
                battleTiles[i].SetUnlocked(false);
            }
        }
    }
}
