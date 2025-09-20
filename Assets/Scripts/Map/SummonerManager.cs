using System.Collections.Generic;
using UnityEngine;

public class SummonerManager : MonoBehaviour {
    public GameObject summonerObject;
    public Summoner summoner;

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

    public static List<SummonerData> GetAvailableSummoners() {
        return new List<SummonerData> {
            new HumanSummoner1().GetData(),
            new HumanSummoner2().GetData(),
            new ElvenSummoner1().GetData(),
            new ElvenSummoner2().GetData(),
            new UndeadSummoner1().GetData(),
            new UndeadSummoner2().GetData(),
            new UnderworldSummoner1().GetData(),
            new UnderworldSummoner2().GetData(),
            };
    }
}