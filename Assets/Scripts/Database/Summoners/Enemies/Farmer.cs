using System;
using System.Collections.Generic;

public class Farmer : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "The Farm remembers",
            health = 15,
            isFriendly = false,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            new Peasant().GetStats(),
            new Peasant().GetStats(),
            new Peasant().GetStats(),
            new Peasant().GetStats(),
            new Peasant().GetStats(),
            new Peasant().GetStats(),
            new MultibowNovice().GetStats(),
            new MultibowNovice().GetStats(),
            new Youngling().GetStats(),
            new Youngling().GetStats(),
            new Squire().GetStats(),
            new Squire().GetStats(),
            new Archer().GetStats(),
            new Archer().GetStats(),
            new Archer().GetStats(),
            new Archer().GetStats(),
            new Watchtower().GetStats(),
            new Watchtower().GetStats(),
            new LegionOfPeasants().GetStats(),
            new LegionOfPeasants().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(SmallHeart);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}