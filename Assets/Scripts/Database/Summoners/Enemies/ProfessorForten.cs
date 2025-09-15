using System;
using System.Collections.Generic;

public class ProfessorForten : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "My warriors are a perfect 4/10!",
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
            new IntrusiveTermite().GetStats(),
            new IntrusiveTermite().GetStats(),
            new IntrusiveTermite().GetStats(),
            new IntrusiveTermite().GetStats(),
            new CrispRat().GetStats(),
            new CrispRat().GetStats(),
            new CrispRat().GetStats(),
            new CrispRat().GetStats(),
            new HardshellPest().GetStats(),
            new HardshellPest().GetStats(),
            new HardshellPest().GetStats(),
            new HardshellPest().GetStats(),
            new CerberusPack().GetStats(),
            new CerberusPack().GetStats(),
            new Damnation().GetStats(),
       };
    }

    void SetItems() {
        Type itemType = typeof(AmuletOfForten);
        ItemManager.enemyItem = ItemManager.GetItemByTitle(itemType.Name);
    }
}