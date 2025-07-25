using System;
using System.Collections.Generic;

public class UncixTheEnlightened : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Draw your best art",
            health = 20,
            isFriendly = false,
            difficulty = 1,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
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
        Type itemType = typeof(ExcitingBook);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}