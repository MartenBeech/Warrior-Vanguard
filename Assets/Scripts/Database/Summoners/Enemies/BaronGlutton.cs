using System;
using System.Collections.Generic;

public class BaronGlutton : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "I like big Warriors and I cannot lie!",
            health = 40,
            isFriendly = false,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            //TODO: Replace with Biggus Warriors
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
            new Mario().GetStats(),
            new Mario().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(BiggusChuggusDoll);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}