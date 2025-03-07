using UnityEngine;
using System.Collections.Generic;

public static class LevelManager {
    private static int currentLevel = 0;
    private static List<MapTile> accessedTiles = new();

    public static void CompleteLevel() {
        currentLevel++;
    }

    public static int GetCurrentLevel() {
        return currentLevel;
    }

    public static void SetCurrentTile(MapTile tile) {
        accessedTiles.Add(tile);
    }

    public static List<MapTile> getAccessedTiles() {
        return accessedTiles;
    }
}
