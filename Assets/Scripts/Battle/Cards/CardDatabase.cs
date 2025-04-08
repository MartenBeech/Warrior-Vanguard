using System.Collections.Generic;
using System.Linq;

public static class CardDatabase {
    public static List<WarriorStats> allCards = new() {
        new ZombieHydra().GetStats(),
        new SkeletonMage().GetStats(),
        new SkeletonRider().GetStats(),
        new SkeletonWarrior().GetStats(),
        new SkeletonArcher().GetStats(),
        new LastBreath().GetStats(),
        new SkinToBones().GetStats(),
        new PoisonPotion().GetStats(),
        new Disarm().GetStats(),
        new Reanimate().GetStats(),
        new AgingCurse().GetStats(),
        new UnholyStorm().GetStats(),
        new Mario().GetStats(),
        new ZombieMinion().GetStats(),
        new Necropolis().GetStats(),
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

    public static WarriorStats GetStatsByTitleAndLevel(string titleAndLevel) {
        string title = titleAndLevel.Split('_')[0];
        string level = titleAndLevel.Split('_')[1];

        WarriorStats stats = allCards.Find(stats => stats.title == title);
        if (stats == null) {
            return null;
        }

        if (level != "0") {
            stats.level = 1;
        }

        return stats;
    }

    public static WarriorStats GetRandomWarriorWithSpecificCost(int cost) {
        List<WarriorStats> warriors = allCards.Where(card => card.cost == cost && card.cardType == CardType.warrior).ToList();
        return warriors[Rng.Range(0, warriors.Count)];
    }
}
