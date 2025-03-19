public class Devil : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = "Devil",
            description = "He's a baaaaaad guy, duh!",
            health = 66,
        };

        stats.healthMax = stats.health;

        return stats;
    }
}