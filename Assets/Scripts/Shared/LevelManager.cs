using UnityEngine;
using System.Collections.Generic;

public static class LevelManager {
    private static int currentLevel = 0;
    private static List<BattleTile> accessedTiles = new();

    public static void CompleteLevel() {
        currentLevel++;
    }

    public static int GetCurrentLevel() {
        return currentLevel;
    }

    public static void SetCurrentTile(BattleTile tile) {
        accessedTiles.Add(tile);
    }

    public static List<BattleTile> getAccessedTiles() {
        return accessedTiles;
    }
}
