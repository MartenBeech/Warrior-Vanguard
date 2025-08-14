using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SummonerSelectionManager : MonoBehaviour {
    public TMP_Text summonerDescriptionText;
    public GameObject summonerSelectionPanel;
    public Button summoner1Button;
    public Button summoner2Button;
    public Button startButton;
    SummonerData[] availableSummoners;
    SummonerData selectedSummoner;
    int summoner1Index;
    int summoner2Index;

    void Start() {
        ToggleSummonerSelectionPanel(false);
        
        availableSummoners = new SummonerData[] {
            new HumanSummoner().GetData(),
            new HumanSummoner2().GetData(),
            new ElvenSummoner().GetData(),
            new ElvenSummoner().GetData(),
            new UndeadSummoner().GetData(),
            new UndeadSummoner().GetData(),
            new UnderworldSummoner().GetData(),
            new UnderworldSummoner().GetData(),
            };
    }

    public void SelectClass(int index) {
        ToggleSummonerSelectionPanel(true);
        startButton.interactable = false;

        summoner1Index = index * 2;
        summoner2Index = (index * 2) + 1;

        summoner1Button.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{availableSummoners[summoner1Index].title}");
        summoner2Button.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{availableSummoners[summoner2Index].title}");
    }

    public void ClickSummoner(int index) {
        startButton.interactable = true;

        summonerDescriptionText.text = index == 0 ?
        availableSummoners[summoner1Index].description :
        availableSummoners[summoner2Index].description;

        selectedSummoner = availableSummoners[index];
        PlayerPrefs.SetInt("SummonerIndex", index);
        PlayerPrefs.Save();
        FriendlySummoner.summonerData = selectedSummoner;

        // Set opacity of clicked button to 1 and the other to 0.5
        Button pressedButton = index == 0 ? summoner1Button : summoner2Button;
        Button otherButton = index == 0 ? summoner2Button : summoner1Button;
        var color = pressedButton.image.color;
        color.a = 1f;
        pressedButton.image.color = color;
        color.a = 0.5f;
        otherButton.image.color = color;
    }

    public void ClickStart() {
        SceneLoader.LoadMap();
    }

    public void ToggleSummonerSelectionPanel(bool shouldShow) {
        if (summonerSelectionPanel) summonerSelectionPanel.SetActive(shouldShow);
    }

    public void ReturnToMainMenu() {
        SceneLoader.LoadMainMenu();
    }
}