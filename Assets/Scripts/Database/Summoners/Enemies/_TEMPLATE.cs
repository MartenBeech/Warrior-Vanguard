using System;
using System.Collections.Generic;

public class SUMMONERTEMPLATE : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "DESCRIPTION",
            health = 0,
            isFriendly = false,
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
        Type itemType = typeof(WoodenSword);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}