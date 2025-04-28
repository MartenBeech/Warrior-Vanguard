using System.Text.RegularExpressions;

public class SummonerStats {
    public string title;
    public string displayTitle;
    public string description;
    public int health;
    public int healthMax;
    public int shield;
    public int skeletonBones = 0;

    public SummonerStats(string title, string description, int health, int healthMax) {
        this.title = title;
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        this.description = description;
        this.health = health;
        this.healthMax = healthMax;
        shield = 0;
    }

    public SummonerStats() {
    }

    public void SetStats(SummonerStats stats) {
        title = stats.title;
        displayTitle = Regex.Replace(stats.title, "(?<!^)([A-Z])", " $1");
        description = stats.description;
        health = stats.health;
        healthMax = stats.healthMax;
        shield = stats.shield;
    }
}