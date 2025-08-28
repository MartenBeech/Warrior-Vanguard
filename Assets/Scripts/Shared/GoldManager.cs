using UnityEngine;

public static class GoldManager {
    private const string GoldKey = "PlayerGold";
    private const string RemoveCardCostKey = "RemoveCardCost";

    public static int gold {
        get => PlayerPrefs.GetInt(GoldKey, 100);
        set {
            PlayerPrefs.SetInt(GoldKey, value);
            PlayerPrefs.Save();
        }
    }

    public static int RemoveCardCost {
        get => PlayerPrefs.GetInt(RemoveCardCostKey, 50);
        set {
            PlayerPrefs.SetInt(RemoveCardCostKey, value);
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
