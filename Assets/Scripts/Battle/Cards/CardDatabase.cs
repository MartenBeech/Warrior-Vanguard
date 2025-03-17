using System.Collections.Generic;

public static class CardDatabase {
    public static List<WarriorStats> allCards = new() {
        new Duck().GetStats(),
        new Mario().GetStats(),
        new BoneConjurer().GetStats(),
        new EldritchSorcerer().GetStats(),
        new FrozenTombcarver().GetStats(),
        new LichQueen().GetStats(),
        new BoneGnawer().GetStats(),
        new FrenziedGhoul().GetStats(),
        new SinisterHowler().GetStats(),
        new ShadowyEntity().GetStats(),
        new BloodMerchant().GetStats(),
        new TheOriginal().GetStats(),
        new VampireApprentice().GetStats(),
        new VampireElder().GetStats(),
        new ZombieMinion().GetStats(),
        new PlagueWalker().GetStats(),
        new CorpseBehemoth().GetStats(),
        new ZombieHydra().GetStats(),
        new Luigi().GetStats(),
        new Mortana().GetStats(),
    };

    public static WarriorStats GetRandomWarriorStats() {
        return allCards[Rng.Range(0, allCards.Count)];
    }
}
