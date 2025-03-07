using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public List<BattleTile> battleTiles;

    private void Start() {
        UpdateTileAccess(LevelManager.GetCurrentLevel());
    }

    private void UpdateTileAccess(int currentLevel) {
        for (int i = 0; i < battleTiles.Count; i++) {
            battleTiles[i].SetInteractable(i == currentLevel);
        }

        for (int i = 0; i < LevelManager.getAccessedTiles().Count; i++) {
            battleTiles[i].MarkAsCompleted();
        }

        // foreach (BattleTile tile in LevelManager.getAccessedTiles()) {
        //     tile.MarkAsCompleted();
        // }
    }
}
