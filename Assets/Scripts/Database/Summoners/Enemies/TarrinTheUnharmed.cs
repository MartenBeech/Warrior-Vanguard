using System;
using System.Collections.Generic;

public class TarrinTheUnharmed : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "I will defend the realm",
            health = 30,
            isFriendly = false,
            difficulty = 1,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            new Squire().GetStats(),
            new Squire().GetStats(),
            new Squire().GetStats(),
            new Squire().GetStats(),
            new Squire().GetStats(),
            new Squire().GetStats(),
            new WatchfulGuard().GetStats(),
            new WatchfulGuard().GetStats(),
            new WatchfulGuard().GetStats(),
            new WatchfulGuard().GetStats(),
            new Knight().GetStats(),
            new Knight().GetStats(),
            new Knight().GetStats(),
            new Knight().GetStats(),
            new Bodyguard().GetStats(),
            new YoungPriestess().GetStats(),
            new YoungPriestess().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(BigArmor);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}