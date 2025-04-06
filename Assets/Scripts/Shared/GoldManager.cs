using UnityEngine;

public static class GoldManager {
    private const string GoldKey = "PlayerGold";

    public static int gold {
        get => PlayerPrefs.GetInt(GoldKey, 100);
        set {
            PlayerPrefs.SetInt(GoldKey, value);
            PlayerPrefs.Save();
        }
    }

    public static void AddGold(int amount) {
        gold += amount;
    }

    public static void RemoveGold(int amount) {
        gold -= amount;
    }

    public static bool SpendGold(int amount) {
        if (gold >= amount) {
            gold -= amount;
            return true;
        }
        return false; // Not enough gold
    }
}
