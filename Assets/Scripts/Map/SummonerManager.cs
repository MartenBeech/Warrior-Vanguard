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
        summoner.SetStats(new SummonerStats(FriendlySummoner.summonerData.title, FriendlySummoner.GetHealth(), FriendlySummoner.GetMaxHealth()));
    }
}