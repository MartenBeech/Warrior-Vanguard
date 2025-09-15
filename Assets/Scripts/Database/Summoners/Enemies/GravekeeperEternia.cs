using System;
using System.Collections.Generic;

public class GravekeeperEternia : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "What is dead may never die",
            health = 20,
            alignment = Alignment.Enemy,
            difficulty = 3,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            new WindDancer().GetStats(),
            new WindDancer().GetStats(),
            new WindDancer().GetStats(),
            new WindDancer().GetStats(),
            new ChillingWraith().GetStats(),
            new ChillingWraith().GetStats(),
            new SoulStealer().GetStats(),
            new VoidBeing().GetStats(),
            new ZombieMinion().GetStats(),
            new ZombieMinion().GetStats(),
            new ZombieMinion().GetStats(),
            new ZombieMinion().GetStats(),
            new PlagueWalker().GetStats(),
            new PlagueWalker().GetStats(),
            new CorpseBehemoth().GetStats(),
            new ZombieHydra().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(ForbiddenLifeElixir);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}