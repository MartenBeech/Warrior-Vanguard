using System;
using System.Collections.Generic;

public class ZerugSwampwalker : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Tread carefully",
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
            new Youngling().GetStats(),
            new Youngling().GetStats(),
            new Youngling().GetStats(),
            new Youngling().GetStats(),
            new Youngling().GetStats(),
            new Youngling().GetStats(),
            new WanderingBirch().GetStats(),
            new WanderingBirch().GetStats(),
            new WanderingBirch().GetStats(),
            new WanderingBirch().GetStats(),
            new BranchManager().GetStats(),
            new BranchManager().GetStats(),
            new BranchManager().GetStats(),
            new BranchManager().GetStats(),
            new UprootedWoods().GetStats(),
            new UprootedWoods().GetStats(),
            new UprootedWoods().GetStats(),
            new UprootedWoods().GetStats(),
            new ElderwoodElder().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(Hourglass);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}