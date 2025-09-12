using UnityEngine;

public static class ExperienceManager {
    public static string TempExpKey = "TempExpKey";
    public static string ExpKey(Genre summonerName) => $"{summonerName}_EXP";
    public static string LevelKey(Genre summonerName) => $"{summonerName}_LEVEL";
    public static string WinsKey(Genre summonerName) => $"{summonerName}_WINS";
    public static int maxLevel = 5;

    public static int GetExperience(Genre summonerName) {
        return PlayerPrefs.GetInt(ExpKey(summonerName), 0);
    }

    public static int GetLevel(Genre summonerName) {
        return PlayerPrefs.GetInt(LevelKey(summonerName), 1);
    }

    public static bool IsMaxLevel(Genre summonerName) {
        return GetLevel(summonerName) >= maxLevel;
    }

    public static int GetXpForNextLevel(Genre summonerName) {
        int level = GetLevel(summonerName);
        if (IsMaxLevel(summonerName)) return 0;
        return level * 100;
    }

    public static void AddTempExperience(int amount) {
        PlayerPrefs.SetInt(TempExpKey, PlayerPrefs.GetInt(TempExpKey, 0) + amount);
        PlayerPrefs.Save();
    }

    public static int GetTempExperience() {
        return PlayerPrefs.GetInt(TempExpKey, 0);
    }

    public static void AddExperience(Genre summonerName, int amount) {
        int exp = GetExperience(summonerName) + amount;
        int level = GetLevel(summonerName);

        while (exp >= GetXpForNextLevel(summonerName) && !IsMaxLevel(summonerName)) {
            exp -= GetXpForNextLevel(summonerName);
            level++;
        }

        PlayerPrefs.SetInt(ExpKey(summonerName), exp);
        PlayerPrefs.SetInt(LevelKey(summonerName), level);
        PlayerPrefs.Save();
    }

    public static void ResetAllLevels() {
        foreach (Genre genre in System.Enum.GetValues(typeof(Genre))) {
            PlayerPrefs.SetInt(ExpKey(genre), 0);
            PlayerPrefs.SetInt(LevelKey(genre), 1);
        }
        PlayerPrefs.Save();
    }

    public static void WinGame(Genre summonerName) {
        PlayerPrefs.SetInt(WinsKey(summonerName), PlayerPrefs.GetInt(WinsKey(summonerName), 0) + 1);
        PlayerPrefs.Save();
    }

    public static int GetWins(Genre summonerName) {
        return PlayerPrefs.GetInt(WinsKey(summonerName), 0);
    }
}