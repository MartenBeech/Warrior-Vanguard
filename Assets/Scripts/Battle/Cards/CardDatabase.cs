using UnityEngine;
using System.Collections.Generic;

public static class CardDatabase {
    public static List<WarriorStats> allCards = new() {
        Duck.GetStats(),
        new BloodMerchant().GetStats(),
        new TheOriginal().GetStats(),
        new VampireApprentice().GetStats(),
        new VampireElder().GetStats(),
        new ZombieMinion().GetStats(),
        new PlagueWalker().GetStats(),
        new CorpseBehemoth().GetStats(),
        new ZombieHydra().GetStats(),
        Mario.GetStats(),
        Luigi.GetStats(),
        Mortana.GetStats()
    };

    public static WarriorStats GetRandomWarriorStats() {
        return allCards[Rng.Range(0, allCards.Count)];
    }
}
