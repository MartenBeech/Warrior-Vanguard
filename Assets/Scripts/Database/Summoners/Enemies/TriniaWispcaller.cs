using System;
using System.Collections.Generic;

public class TriniaWispcaller : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "I hear the 'wispers' of the forest",
            health = 30,
            isFriendly = false,
            difficulty = 2,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            new Mario().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(LanternOfWisps);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}