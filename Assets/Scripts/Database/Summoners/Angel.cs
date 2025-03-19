public class Angel : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = "Angel",
            description = "Just a chill guy",
            health = 42,
        };

        stats.healthMax = stats.health;

        return stats;
    }
}