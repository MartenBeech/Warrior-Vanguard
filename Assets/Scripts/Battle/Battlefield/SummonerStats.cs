public class SummonerStats {
    public string title;
    public string description;
    public int health;
    public int healthMax;
    public int skeletonBones = 0;

    public SummonerStats(string title, string description, int health, int healthMax) {
        this.title = title;
        this.description = description;
        this.health = health;
        this.healthMax = healthMax;
    }

    public SummonerStats() {
    }

    public void SetStats(SummonerStats stats) {
        title = stats.title;
        description = stats.description;
        health = stats.health;
        healthMax = stats.healthMax;
    }
}