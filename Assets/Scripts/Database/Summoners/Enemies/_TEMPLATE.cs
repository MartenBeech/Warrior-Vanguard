using System;
using System.Collections.Generic;
using System.Linq;

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
        Type[] itemTypes = new Type[] {
            typeof(WoodenSword),
        };

        List<Item> items = itemTypes.Select(type => ItemManager.GetItemByTitle(type.Name)).ToList();
        SetEnemyItems(items);
    }
}