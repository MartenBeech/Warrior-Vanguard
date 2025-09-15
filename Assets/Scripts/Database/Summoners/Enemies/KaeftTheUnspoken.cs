using System;
using System.Collections.Generic;

public class KaeftTheUnspoken : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "Shhhhhhh",
            health = 20,
            alignment = Alignment.Enemy,
            difficulty = 1,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
            //TODO: Add deck of Librarians And Demons
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
        Type itemType = typeof(Shush);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}