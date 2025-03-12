using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EventManager : MonoBehaviour {
    public TMP_Text eventText;
    public int goldReward = 50;

    private bool goldClaimed = false;

    void Start() {
        eventText.text = "You found a hidden treasure! Claim your gold reward!";
    }

    public void ClaimGold() {
        if (!goldClaimed) {
            GoldManager.AddGold(goldReward);
            goldClaimed = true;
            eventText.text = $"You gained {goldReward} gold!";
        }
    }

    public void ReturnToMap() {
        SceneLoader.LoadMap();
    }
}
