using System.Collections.Generic;

public class SkeletonKing : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "He finna bone you up mate!",
            health = 42,
            isFriendly = false,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        return new List<WarriorStats>() {
           new SkeletonMage().GetStats(),
           new SkeletonMage().GetStats(),
           new SkeletonMage().GetStats(),
           new SkeletonMage().GetStats(),
           new SkeletonMage().GetStats(),
           new SkeletonRider().GetStats(),
           new SkeletonRider().GetStats(),
           new SkeletonRider().GetStats(),
           new SkeletonRider().GetStats(),
           new SkeletonRider().GetStats(),
           new SkeletonWarrior().GetStats(),
           new SkeletonWarrior().GetStats(),
           new SkeletonWarrior().GetStats(),
           new SkeletonWarrior().GetStats(),
           new SkeletonWarrior().GetStats(),
           new SkeletonArcher().GetStats(),
           new SkeletonArcher().GetStats(),
           new SkeletonArcher().GetStats(),
           new SkeletonArcher().GetStats(),
           new SkeletonArcher().GetStats(),
       };
    }
}