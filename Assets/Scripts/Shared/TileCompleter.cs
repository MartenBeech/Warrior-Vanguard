using UnityEngine;

public static class TileCompleter {
    public static string currentTileIndex;

    public static void MarkTileAsCompleted(bool isCompleted, string tileIndex) {
        if (tileIndex != null) {
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