using System.Collections.Generic;

public static class CardDatabase {
    public static List<WarriorStats> allCards = new() {
        new Duck().GetStats(),
        new Mario().GetStats(),
        new Necropolis().GetStats(),
        new ZombieHydra().GetStats(),
        new SoulStealer().GetStats(),
        new VileMutation().GetStats(),
        new CorpseBehemoth().GetStats(),
        new LichQueen().GetStats(),
        new BloodMerchant().GetStats(),
        new PileOfBones().GetStats(),
        new WailingWall().GetStats(),
        new VoidBeing().GetStats(),
        new ChillingWraith().GetStats(),
        new WindDancer().GetStats(),
        new BoneConjurer().GetStats(),
        new EldritchSorcerer().GetStats(),
        new FrozenTombcarver().GetStats(),
        new BoneGnawer().GetStats(),
        new FrenziedGhoul().GetStats(),
        new SinisterHowler().GetStats(),
        new ShadowyEntity().GetStats(),
        new TheOriginal().GetStats(),
        new VampireApprentice().GetStats(),
        new VampireElder().GetStats(),
        new ZombieMinion().GetStats(),
        new PlagueWalker().GetStats(),
        new Luigi().GetStats(),
        new Mortana().GetStats(),
    };

    public static WarriorStats GetRandomWarriorStats(CardRarity rarity = CardRarity.None) {
        if (rarity == CardRarity.None) {
            return allCards[Rng.Range(0, allCards.Count)];
        }

        List<WarriorStats> filteredCards = allCards.FindAll(card => card.rarity == rarity);
        return filteredCards[Rng.Range(0, filteredCards.Count)];
    }
}
