using UnityEngine;

public class SummonerManager : MonoBehaviour {
    public GameObject summonerObject;
    Summoner summoner;

    void Start() {
        summoner = summonerObject.GetComponent<Summoner>();
        UpdateSummonerUi();
    }

    public void GainHealth(int health) {
        FriendlySummoner.GainHealth(health);
        UpdateSummonerUi();
    }

    public void LoseHealth(int health) {
        FriendlySummoner.LoseHealth(health);
        UpdateSummonerUi();
    }

    public void GainMaxHealth(int health) {
        FriendlySummoner.GainMaxHealth(health);
        UpdateSummonerUi();
    }

    public void LoseMaxHealth(int health) {
        FriendlySummoner.LoseMaxHealth(health);
        UpdateSummonerUi();
    }

    private void UpdateSummonerUi() {
        string summonerTitle = PlayerPrefs.GetString("SelectedSummoner");
        summoner.SetStats(new SummonerStats(summonerTitle, FriendlySummoner.currentHealth, FriendlySummoner.maxHealth, true));
    }
}