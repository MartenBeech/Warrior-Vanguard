using System;
using System.Collections.Generic;

public class BonestackerBanya : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Lovely Boooones!",
            health = 20,
            isFriendly = false,
            difficulty = 3,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
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
           new EldritchSorcerer().GetStats(),
           new EldritchSorcerer().GetStats(),
           new BoneConjurer().GetStats(),
           new LichQueen().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(PairOfBones);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}