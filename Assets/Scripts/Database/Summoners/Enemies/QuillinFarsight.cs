using System;
using System.Collections.Generic;

public class QuillinFarsight : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "I see you!",
            health = 20,
            alignment = Alignment.Enemy,
            difficulty = 2,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            //TODO: Replace with long range Warriors
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
            new Mario().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(EagleEye);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}