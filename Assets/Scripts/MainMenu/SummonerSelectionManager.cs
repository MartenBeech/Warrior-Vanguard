using UnityEngine;

public class SummonerSelectionManager : MonoBehaviour {
    public SummonerData[] availableSummoners;
    private SummonerData selectedSummoner;

    void Start() {
        SummonerData humanSummoner = new SummonerData("HumanSummoner");
        SummonerData elvenSummoner = new SummonerData("ElvenSummoner");
        SummonerData undeadSummoner = new SummonerData("UndeadSummoner");
        SummonerData underworldSummoner = new SummonerData("UnderworldSummoner");
        availableSummoners = new SummonerData[] { humanSummoner, elvenSummoner, undeadSummoner, underworldSummoner };
    }

    public void SelectSummoner(int index) {
        selectedSummoner = availableSummoners[index];
        PlayerPrefs.SetString("SelectedSummoner", selectedSummoner.title);
        SceneLoader.LoadMap();
    }

    public void ReturnToMainMenu() {
        SceneLoader.LoadMainMenu();
    }
}
