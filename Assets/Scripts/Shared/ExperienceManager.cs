using UnityEngine;

public static class ExperienceManager {
    public static string ExpKey(Genre summonerName) => $"{summonerName}_EXP";
    public static string LevelKey(Genre summonerName) => $"{summonerName}_LEVEL";

    public static int GetExperience(Genre summonerName) {
        return PlayerPrefs.GetInt(ExpKey(summonerName), 0);
    }

    public static int GetLevel(Genre summonerName) {
        return PlayerPrefs.GetInt(LevelKey(summonerName), 1);
    }

    public static int GetXpForNextLevel(Genre summonerName) {
        int level = GetLevel(summonerName);

        return level * 100;
    }

    public static void AddExperience(Genre summonerName, int amount) {
        int exp = GetExperience(summonerName) + amount;
        int level = GetLevel(summonerName);

        while (exp >= GetXpForNextLevel(summonerName) && level < 5) {
            exp -= GetXpForNextLevel(summonerName);
            level++;
        }

        PlayerPrefs.SetInt(ExpKey(summonerName), exp);
        PlayerPrefs.SetInt(LevelKey(summonerName), level);
        PlayerPrefs.Save();
    }
}