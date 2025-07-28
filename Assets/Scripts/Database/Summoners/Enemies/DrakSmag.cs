using System;
using System.Collections.Generic;

public class DrakSmag : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Buuuuurn!",
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
            new MinotaurLord().GetStats(),
            new MinotaurKing().GetStats(),
            new Phoenix().GetStats(),
            new GoldDragon().GetStats(),
            new GhostDragon().GetStats(),
            new BoneDragon().GetStats(),
            new BlackDragon().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(DragonDiscount);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}