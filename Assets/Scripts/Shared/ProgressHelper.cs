using UnityEngine;

public class ProgressHelper {
    public static string WinsKey(Genre summonerName) => $"{summonerName}_WINS";
    public static void WinGame(Genre summonerName) {
        PlayerPrefs.SetInt(WinsKey(summonerName), PlayerPrefs.GetInt(WinsKey(summonerName), 0) + 1);
        PlayerPrefs.Save();
    }

    public static int GetWins(Genre summonerName) {
        return PlayerPrefs.GetInt(WinsKey(summonerName), 0);
    }

    public static int GetWins() {
        int wins = 0;
        foreach (Genre genre in System.Enum.GetValues(typeof(Genre))) {
            wins += GetWins(genre);
        }
        return wins;
    }
}