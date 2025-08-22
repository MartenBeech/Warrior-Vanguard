using UnityEngine;
using System.Collections.Generic;

public static class LevelManager {
    private static List<MapTile> accessedTiles = new();
    public static bool isAlive = true;

    public static void CompleteLevel() {
        TileCompleter.MarkTileAsCompleted();
        GoldManager.AddGold(50);
        SceneLoader.LoadMap();
        ItemManager.enemyItem = null;
    }

    public static void LoseLevel() {
        isAlive = false;
        SceneLoader.LoadMap();
    }

    public static void SetCurrentTile(MapTile tile) {
        accessedTiles.Add(tile);
    }

    public static List<MapTile> getAccessedTiles() {
        return accessedTiles;
    }
}
