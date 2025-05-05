using UnityEngine;

public static class TileCompleter {
    static int currentTileIndex = -1;

    public static void MarkTileAsCompleted(bool isCompleted = true, int tileIndex = -1) {        
        if (tileIndex > -1) {
            currentTileIndex = tileIndex;
        }

        if (isCompleted) {            
            ClearLastCompleted();
            PlayerPrefs.SetInt($"TileCompleted_{currentTileIndex}", 1);
            PlayerPrefs.SetInt($"LastCompleted_{currentTileIndex}", 1);
            PlayerPrefs.DeleteKey($"TileActive");
            PlayerPrefs.Save();
        }
    }

    static void ClearLastCompleted() {
        for (int i = 0; i < 20; i++) {
            PlayerPrefs.DeleteKey($"LastCompleted_{i}");
        }
    }
}