using System;
using System.Collections.Generic;

public class Cavalier : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Chaarge!",
            health = 20,
            isFriendly = false,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            new UnicornFoal().GetStats(),
            new UnicornFoal().GetStats(),
            new UnicornFoal().GetStats(),
            new UnicornFoal().GetStats(),
            new LightPiercer().GetStats(),
            new LightPiercer().GetStats(),
            new LightPiercer().GetStats(),
            new LightPiercer().GetStats(),
            new ForestPrism().GetStats(),
            new CentaurWarrior().GetStats(),
            new CentaurWarrior().GetStats(),
            new CentaurWarrior().GetStats(),
            new CentaurWarrior().GetStats(),
            new Centaura().GetStats(),
            new SkeletonRider().GetStats(),
            new SkeletonRider().GetStats(),
            new SkeletonRider().GetStats(),
            new SkeletonRider().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(WoodenSword);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}