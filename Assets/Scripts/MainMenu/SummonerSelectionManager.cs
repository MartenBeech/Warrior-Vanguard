using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SummonerSelectionManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public SummonerData[] availableSummoners;
    private SummonerData selectedSummoner;
    public TMP_Text descriptionText;
    public int summonerIndex;

    void Start() {
        SummonerData humanSummoner = new("HumanSummoner", "A Human Summoner with balanced stats");
        SummonerData elvenSummoner = new("ElvenSummoner", "An Elven Summoner with agility and magic");
        SummonerData undeadSummoner = new("UndeadSummoner", "An Undead Summoner with dark powers");
        SummonerData underworldSummoner = new("UnderworldSummoner", "A Summoner from the Underworld with unique abilities");
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

    public void OnPointerEnter(PointerEventData eventData) {
        descriptionText.text = availableSummoners[summonerIndex].description;
    }

    public void OnPointerExit(PointerEventData eventData) {
        descriptionText.text = "";
    }
}