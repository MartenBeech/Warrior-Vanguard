public class SummonerStats {
    public string title;
    public string description;
    public int health;
    public int healthMax;

    public void SetStats(SummonerStats stats) {
        title = stats.title;
        description = stats.description;
        health = stats.health;
        healthMax = stats.healthMax;
    }
}