using System;
using System.Collections.Generic;

public class BonestackerBanya : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Lovely Boooones!",
            health = 20,
            isFriendly = false,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            //TODO: Replace with skeleton and liches
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(PairOfBones);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}