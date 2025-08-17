
using System.Collections.Generic;

public static class StartingDecks {
    public enum SummonerType {
        Human1,
        Human2,
        Elven1,
        Elven2,
        Undead1,
        Undead2,
        Underworld1,
        Underworld2
    }

    public static void SetStartingDeck(string summonerType) {
        Card card1 = new();
        Card card2 = new();
        Card card3 = new();
        Card card4 = new();
        Card card5 = new();
        Card card6 = new();
        Card card7 = new();
        Card card8 = new();
        Card card9 = new();
        Card card10 = new();
        Card card11 = new();
        Card card12 = new();
        Card card13 = new();
        Card card14 = new();
        Card card15 = new();

        switch (summonerType) {
            case "HumanSummoner1":
                card1.SetStats(new Peasant().GetStats());
                card2.SetStats(new Peasant().GetStats());
                card3.SetStats(new Defender().GetStats());
                card4.SetStats(new Defender().GetStats());
                card5.SetStats(new Squire().GetStats());
                card6.SetStats(new Archer().GetStats());
                card7.SetStats(new Archer().GetStats());
                card8.SetStats(new YoungPriestess().GetStats());
                card9.SetStats(new HeavyRider().GetStats());
                card10.SetStats(new DrawfulHobbyist().GetStats());
                card11.SetStats(new MarketingManager().GetStats());
                card12.SetStats(new HolyFlame().GetStats());
                card13.SetStats(new HolyFlame().GetStats());
                card14.SetStats(new GuidingStrength().GetStats());
                card15.SetStats(new HolyEmpower().GetStats());
                break;
            case "HumanSummoner2":
                card1.SetStats(new Peasant().GetStats());
                card2.SetStats(new Peasant().GetStats());
                card3.SetStats(new Defender().GetStats());
                card4.SetStats(new Defender().GetStats());
                card5.SetStats(new Squire().GetStats());
                card6.SetStats(new Archer().GetStats());
                card7.SetStats(new Archer().GetStats());
                card8.SetStats(new YoungPriestess().GetStats());
                card9.SetStats(new HeavyRider().GetStats());
                card10.SetStats(new DrawfulHobbyist().GetStats());
                card11.SetStats(new MarketingManager().GetStats());
                card12.SetStats(new HolyFlame().GetStats());
                card13.SetStats(new HolyFlame().GetStats());
                card14.SetStats(new GuidingStrength().GetStats());
                card15.SetStats(new HolyEmpower().GetStats());
                break;
            case "ElvenSummoner1":
                card1.SetStats(new Youngling().GetStats());
                card2.SetStats(new Youngling().GetStats());
                card3.SetStats(new Watchtower().GetStats());
                card4.SetStats(new CentaurWarrior().GetStats());
                card5.SetStats(new ClubCrasher().GetStats());
                card6.SetStats(new WoodElf().GetStats());
                card7.SetStats(new WoodElf().GetStats());
                card8.SetStats(new BranchManager().GetStats());
                card9.SetStats(new WerewolfRunner().GetStats());
                card10.SetStats(new ShinyButterfly().GetStats());
                card11.SetStats(new Pinkxie().GetStats());
                card12.SetStats(new Embiggen().GetStats());
                card13.SetStats(new Embiggen().GetStats());
                card14.SetStats(new Thorns().GetStats());
                card15.SetStats(new Haste().GetStats());
                break;
            case "ElvenSummoner2":
                card1.SetStats(new Youngling().GetStats());
                card2.SetStats(new Youngling().GetStats());
                card3.SetStats(new Watchtower().GetStats());
                card4.SetStats(new CentaurWarrior().GetStats());
                card5.SetStats(new ClubCrasher().GetStats());
                card6.SetStats(new WoodElf().GetStats());
                card7.SetStats(new WoodElf().GetStats());
                card8.SetStats(new BranchManager().GetStats());
                card9.SetStats(new WerewolfRunner().GetStats());
                card10.SetStats(new ShinyButterfly().GetStats());
                card11.SetStats(new Pinkxie().GetStats());
                card12.SetStats(new Embiggen().GetStats());
                card13.SetStats(new Embiggen().GetStats());
                card14.SetStats(new Thorns().GetStats());
                card15.SetStats(new Haste().GetStats());
                break;
            case "UndeadSummoner1":
                card1.SetStats(new ZombieMinion().GetStats());
                card2.SetStats(new ZombieMinion().GetStats());
                card3.SetStats(new BoneGnawer().GetStats());
                card4.SetStats(new EldritchSorcerer().GetStats());
                card5.SetStats(new SkeletonArcher().GetStats());
                card6.SetStats(new SkeletonArcher().GetStats());
                card7.SetStats(new SkeletonWarrior().GetStats());
                card8.SetStats(new VampireApprentice().GetStats());
                card9.SetStats(new WindDancer().GetStats());
                card10.SetStats(new DarkRider().GetStats());
                card11.SetStats(new DarkRider().GetStats());
                card12.SetStats(new PoisonPotion().GetStats());
                card13.SetStats(new PoisonPotion().GetStats());
                card14.SetStats(new Arise().GetStats());
                card15.SetStats(new AgingCurse().GetStats());
                break;
            case "UndeadSummoner2":
                card1.SetStats(new ZombieMinion().GetStats());
                card2.SetStats(new ZombieMinion().GetStats());
                card3.SetStats(new BoneGnawer().GetStats());
                card4.SetStats(new EldritchSorcerer().GetStats());
                card5.SetStats(new SkeletonArcher().GetStats());
                card6.SetStats(new SkeletonArcher().GetStats());
                card7.SetStats(new SkeletonWarrior().GetStats());
                card8.SetStats(new VampireApprentice().GetStats());
                card9.SetStats(new WindDancer().GetStats());
                card10.SetStats(new DarkRider().GetStats());
                card11.SetStats(new DarkRider().GetStats());
                card12.SetStats(new PoisonPotion().GetStats());
                card13.SetStats(new PoisonPotion().GetStats());
                card14.SetStats(new Arise().GetStats());
                card15.SetStats(new AgingCurse().GetStats());
                break;
            case "UnderworldSummoner1":
                card1.SetStats(new FlameWarden().GetStats());
                card2.SetStats(new FriendlyFiend().GetStats());
                card3.SetStats(new RockThrower().GetStats());
                card4.SetStats(new RockThrower().GetStats());
                card5.SetStats(new SketchySketcher().GetStats());
                card6.SetStats(new HappyHarpy().GetStats());
                card7.SetStats(new IntrusiveTermite().GetStats());
                card8.SetStats(new IntrusiveTermite().GetStats());
                card9.SetStats(new CrispRat().GetStats());
                card10.SetStats(new HellHound().GetStats());
                card11.SetStats(new HellHound().GetStats());
                card12.SetStats(new Firebolt().GetStats());
                card13.SetStats(new Firebolt().GetStats());
                card14.SetStats(new MoltenBlade().GetStats());
                card15.SetStats(new RainOfFire().GetStats());
                break;
            case "UnderworldSummoner2":
                card1.SetStats(new FlameWarden().GetStats());
                card2.SetStats(new FriendlyFiend().GetStats());
                card3.SetStats(new RockThrower().GetStats());
                card4.SetStats(new RockThrower().GetStats());
                card5.SetStats(new SketchySketcher().GetStats());
                card6.SetStats(new HappyHarpy().GetStats());
                card7.SetStats(new IntrusiveTermite().GetStats());
                card8.SetStats(new IntrusiveTermite().GetStats());
                card9.SetStats(new CrispRat().GetStats());
                card10.SetStats(new HellHound().GetStats());
                card11.SetStats(new HellHound().GetStats());
                card12.SetStats(new Firebolt().GetStats());
                card13.SetStats(new Firebolt().GetStats());
                card14.SetStats(new MoltenBlade().GetStats());
                card15.SetStats(new RainOfFire().GetStats());
                break;
        }

        DeckManager.AddCard(card1);
        DeckManager.AddCard(card2);
        DeckManager.AddCard(card3);
        DeckManager.AddCard(card4);
        DeckManager.AddCard(card5);
        DeckManager.AddCard(card6);
        DeckManager.AddCard(card7);
        DeckManager.AddCard(card8);
        DeckManager.AddCard(card9);
        DeckManager.AddCard(card10);
        DeckManager.AddCard(card11);
        DeckManager.AddCard(card12);
        DeckManager.AddCard(card13);
        DeckManager.AddCard(card14);
        DeckManager.AddCard(card15);
    }
}