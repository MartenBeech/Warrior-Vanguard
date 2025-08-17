using UnityEngine;

public static class ContinueManager {
    public static void LoadSummoner() {
        SummonerData[] availableSummoners = new SummonerData[] {
            new HumanSummoner1().GetData(),
            new HumanSummoner2().GetData(),
            new ElvenSummoner1().GetData(),
            new ElvenSummoner2().GetData(),
            new UndeadSummoner1().GetData(),
            new UndeadSummoner2().GetData(),
            new UnderworldSummoner1().GetData(),
            new UnderworldSummoner2().GetData(),
            };

        int index = PlayerPrefs.GetInt("SummonerIndex", 0);
        SummonerData selectedSummoner = availableSummoners[index];
        FriendlySummoner.summonerData = selectedSummoner;
    }

    public static void ReturnToMainMenu() {
        SceneLoader.LoadMainMenu();
    }
}