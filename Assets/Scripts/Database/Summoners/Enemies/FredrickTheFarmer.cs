using System;
using System.Collections.Generic;

public class FredrickTheFarmer : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "The Farm remembers",
            health = 15,
            alignment = Alignment.Enemy,
            difficulty = 1,
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
            new AngryMob().GetStats(),
            new AngryMob().GetStats(),
            new AngryMob().GetStats(),
            new MultibowNovice().GetStats(),
            new MultibowNovice().GetStats(),
            new Youngling().GetStats(),
            new Youngling().GetStats(),
            new Squire().GetStats(),
            new Squire().GetStats(),
            // new Archer().GetStats(),
            // new Archer().GetStats(),
            // new Archer().GetStats(),
            new Watchtower().GetStats(),
            new Watchtower().GetStats(),
            new CherryPicker().GetStats(),
            new CherryPicker().GetStats(),
            new CombineHarvester().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(WoodenSword);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}