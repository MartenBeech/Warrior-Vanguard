using System.Collections.Generic;

public class BabyBoss : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "I'M OWNWY THWEE",
            health = 3,
            isFriendly = false,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        return new List<WarriorStats>() {
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
            new Mario().GetStats(),
            new Luigi().GetStats(),
       };
    }
}