using UnityEngine;

public static class ContinueManager {
    public static void LoadSummoner() {
        SummonerData[] availableSummoners = new SummonerData[] {
            new HumanSummoner().GetData(),
            new HumanSummoner2().GetData(),
            new ElvenSummoner().GetData(),
            new ElvenSummoner().GetData(),
            new UndeadSummoner().GetData(),
            new UndeadSummoner().GetData(),
            new UnderworldSummoner().GetData(),
            new UnderworldSummoner().GetData(),
            };

        int index = PlayerPrefs.GetInt("SummonerIndex", 0);
        SummonerData selectedSummoner = availableSummoners[index];
        FriendlySummoner.summonerData = selectedSummoner;
    }

    public static void ReturnToMainMenu() {
        SceneLoader.LoadMainMenu();
    }
}