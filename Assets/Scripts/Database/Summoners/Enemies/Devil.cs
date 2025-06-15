using System;
using System.Collections.Generic;
using System.Linq;

public class Devil : SummonerStats {
    public SummonerStats GetSummoner() {
        SummonerStats stats = new() {
            title = GetType().Name,
            description = "He's a baaaaaad guy, duh!",
            health = 66,
            isFriendly = false,
        };
        stats.healthMax = stats.health;
        return stats;
    }

    public List<WarriorStats> GetDeck() {
        SetItems();
        return new List<WarriorStats>() {
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
    }

    void SetItems() {
        Type[] itemTypes = new Type[] {
            typeof(WoodenSword),
        };
        
        List<Item> items = itemTypes.Select(type => ItemManager.GetItemByTitle(type.Name)).ToList();
        SetEnemyItems(items);
    }
}