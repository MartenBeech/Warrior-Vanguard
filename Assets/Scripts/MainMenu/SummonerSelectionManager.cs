using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SummonerSelectionManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public SummonerData[] availableSummoners;
    private SummonerData selectedSummoner;
    public TMP_Text descriptionText;
    public int summonerIndex;

    void Start() {
        availableSummoners = new SummonerData[] {
            new HumanSummoner().GetData(),
            new ElvenSummoner().GetData(),
            new UndeadSummoner().GetData(),
            new UnderworldSummoner().GetData(),
            };
    }

    public void SelectSummoner(int index) {
        selectedSummoner = availableSummoners[index];
        PlayerPrefs.SetInt("SummonerIndex", index);
        PlayerPrefs.Save();
        FriendlySummoner.summonerData = selectedSummoner;
        SceneLoader.LoadMap();
    }

    public void LoadSummoner() {
        int index = PlayerPrefs.GetInt("SummonerIndex", 0);
        selectedSummoner = availableSummoners[index];
        FriendlySummoner.summonerData = selectedSummoner;
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