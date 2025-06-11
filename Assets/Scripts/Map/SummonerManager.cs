using UnityEngine;

public class SummonerManager : MonoBehaviour {
    public GameObject summonerObject;
    Summoner summoner;
    
    void Start() {
        summoner = summonerObject.GetComponent<Summoner>();
        UpdateSummonerUi();
    }

    public void GainHealth(int health) {
        Angel.GainHealth(health);
        UpdateSummonerUi();
    }

    public void LoseHealth(int health) {
        Angel.LoseHealth(health);
        UpdateSummonerUi();
    }

    public void GainMaxHealth(int health) {
        Angel.GainMaxHealth(health);
        UpdateSummonerUi();
    }

    public void LoseMaxHealth(int health) {
        Angel.LoseMaxHealth(health);
        UpdateSummonerUi();
    }

    private void UpdateSummonerUi() {
        string summonerTitle = PlayerPrefs.GetString("SelectedSummoner");
        summoner.SetStats(new SummonerStats(summonerTitle, Angel.currentHealth, Angel.maxHealth));
    }
}