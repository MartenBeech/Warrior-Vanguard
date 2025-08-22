using UnityEngine;

public static class TileCompleter {
    public static string currentTileIndex;

    public static void MarkTileAsCompleted() {
        ClearLastCompleted();
        PlayerPrefs.SetInt($"TileCompleted_{currentTileIndex}", 1);
        PlayerPrefs.SetInt($"LastCompleted_{currentTileIndex}", 1);
        PlayerPrefs.DeleteKey($"TileActive");
        PlayerPrefs.Save();
    }

    public static void ClearLastCompleted() {
        if (currentTileIndex != null) {
            PlayerPrefs.DeleteKey($"LastCompleted_{currentTileIndex}");
        }
    }
}