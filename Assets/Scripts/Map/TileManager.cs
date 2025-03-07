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
        for (int i = 0; i < battleTiles.Count; i++) {
            bool isCompleted = PlayerPrefs.GetInt($"TileCompleted_{i}", 0) == 1;
            battleTiles[i].SetInteractable(i == LevelManager.GetCurrentLevel());

            if (isCompleted) {
                battleTiles[i].MarkAsCompleted();
            }
        }
    }

    public void MarkTileAsCompleted(MapTile tile) {
        if (battleTiles.Contains(tile)) {
            int tileIndex = battleTiles.IndexOf(tile);
            PlayerPrefs.SetInt($"TileCompleted_{tileIndex}", 1);
            PlayerPrefs.Save();
        }
    }
}
