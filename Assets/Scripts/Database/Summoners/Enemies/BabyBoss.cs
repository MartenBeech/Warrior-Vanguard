using System;
using System.Collections.Generic;
using UnityEngine;

public class BabyBoss : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "I'M OWNWY THWEE",
            health = 3,
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

    void SetItems() {
        Type itemType = typeof(SmallHeart);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}