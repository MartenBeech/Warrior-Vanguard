using System.Collections.Generic;

public class SUMMONERTEMPLATE : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "DESCRIPTION",
            health = 0,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        return new List<WarriorStats>() {
            new Mario().GetStats(),
       };
    }
}