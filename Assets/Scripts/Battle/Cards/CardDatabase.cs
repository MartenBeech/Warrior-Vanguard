using System.Collections.Generic;
using System.Linq;

public static class CardDatabase {
    public static List<WarriorStats> allCards = new() {
        new GambleGimple().GetStats(),
        new FavoriteChild().GetStats(),
        new FriendlyFiend().GetStats(),
        new ArtProfessorArnold().GetStats(),
        new PencilCraftsman().GetStats(),
        new DrawfulHobbyist().GetStats(),
        new PactConjuring().GetStats(),
        new PentagramEscapee().GetStats(),
        new DemonicAbomination().GetStats(),
        new ArchPainmaker().GetStats(),
        new Mario().GetStats(),
        new Sanctuary().GetStats(),
        new DivineShield().GetStats(),
        new Parasite().GetStats(),
        new SpiritualGrounds().GetStats(),
        new MountainDragon().GetStats(),
        new InternationalLibrary().GetStats(),
        new DragonLeader().GetStats(),
        new LightDragon().GetStats(),
        new Battlemonk().GetStats(),
        new LocalBookkeeper().GetStats(),
        new MarketingManager().GetStats(),
        new HRSusan().GetStats(),
        new KarenTheLibrarian().GetStats(),
        new FencingRookie().GetStats(),
        new Vitalblade().GetStats(),
        new GrandDualist().GetStats(),
        new UnmatchedEpeewielder().GetStats(),
        new HellHound().GetStats(),
        new MeltingMagma().GetStats(),
        new FieryAvatar().GetStats(),
        new SuccubusSeducer().GetStats(),
        new FlameWarden().GetStats(),
        new AidFromTheSpirits().GetStats(),
        new CatReflexes().GetStats(),
        new Blind().GetStats(),
        new SharpSight().GetStats(),
        new Thorns().GetStats(),
        new Haste().GetStats(),
        new Embiggen().GetStats(),
        new TimeHealsAllWounds().GetStats(),
        new GuardianAngel().GetStats(),
        new InfernalShooter().GetStats(),
        new HadesCompanion().GetStats(),
        new CerberusPack().GetStats(),
        new Firebreather().GetStats(),
        new Damnation().GetStats(),
        new HardshellPest().GetStats(),
        new CrispRat().GetStats(),
        new IntrusiveTermite().GetStats(),
        new Stormseer().GetStats(),
        new MargeTheCharged().GetStats(),
        new LightningCaller().GetStats(),
        new Apprentice().GetStats(),
        new UnstableExplosives().GetStats(),
        new TearsOfTheLight().GetStats(),
        new HolyEmpower().GetStats(),
        new HolyFlame().GetStats(),
        new GuidingStrength().GetStats(),
        new Fireball().GetStats(),
        new Armageddon().GetStats(),
        new RainOfFire().GetStats(),
        new Firebolt().GetStats(),
        new DiveBomber().GetStats(),
        new GriffinCombatant().GetStats(),
        new SkyGlider().GetStats(),
        new EarlyBird().GetStats(),
        new SirSpearmint().GetStats(),
        new MortalLance().GetStats(),
        new HeavyRider().GetStats(),
        new GoodKnight().GetStats(),
        new Bodyguard().GetStats(),
        new WatchfulGuard().GetStats(),
        new Defender().GetStats(),
        new PoiSonRogue().GetStats(),
        new WailingWall().GetStats(),
        new SuspiciousGargoyle().GetStats(),
        new ThirdEyeHarpy().GetStats(),
        new HappyHarpy().GetStats(),
        new FierceIronclaw().GetStats(),
        new SkeletonMage().GetStats(),
        new SkeletonRider().GetStats(),
        new SkeletonWarrior().GetStats(),
        new SkeletonArcher().GetStats(),
        new CorruptedSprite().GetStats(),
        new GlisteringFairy().GetStats(),
        new Pinkxie().GetStats(),
        new ShinyButterfly().GetStats(),
        new Whitefur().GetStats(),
        new WerewolfRunner().GetStats(),
        new NightHunter().GetStats(),
        new MoonProwler().GetStats(),
        new RejuvenatingOak().GetStats(),
        new ThornBush().GetStats(),
        new LightkeeperZealot().GetStats(),
        new BountyHunter().GetStats(),
        new LuckyLooter().GetStats(),
        new YoungPriestess().GetStats(),
        new WiseMonk().GetStats(),
        new Watchtower().GetStats(),
        new Archer().GetStats(),
        new HoodyRobin().GetStats(),
        new Knight().GetStats(),
        new LegionOfPeasants().GetStats(),
        new Peasant().GetStats(),
        new Squire().GetStats(),
        new Gunslinger().GetStats(),
        new FlameThrower().GetStats(),
        new RockThrower().GetStats(),
        new MinotaurBaby().GetStats(),
        new MinotaurKing().GetStats(),
        new MinotaurLord().GetStats(),
        new ElderwoodElder().GetStats(),
        new UprootedWoods().GetStats(),
        new WanderingBirch().GetStats(),
        new BranchManager().GetStats(),
        new ForestTroll().GetStats(),
        new TrollKing().GetStats(),
        new Grumpy().GetStats(),
        new ClubCrasher().GetStats(),
        new Mario().GetStats(),
        new Phoenix().GetStats(),
        new GhostDragon().GetStats(),
        new BlackDragon().GetStats(),
        new GoldDragon().GetStats(),
        new BoneDragon().GetStats(),
        new Reanimate().GetStats(),
        new Centaura().GetStats(),
        new CentaurWarrior().GetStats(),
        new GreedyDwarf().GetStats(),
        new Youngling().GetStats(),
        new MultibowNovice().GetStats(),
        new LongbowGrandmaster().GetStats(),
        new WoodElf().GetStats(),
        new ElvenArcher().GetStats(),
        new RadiantProtector().GetStats(),
        new ForestPrism().GetStats(),
        new LightPiercer().GetStats(),
        new UnicornFoal().GetStats(),
        new ZombieHydra().GetStats(),
        new LastBreath().GetStats(),
        new SkinToBones().GetStats(),
        new PoisonPotion().GetStats(),
        new Disarm().GetStats(),
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
            return Rng.Entry(allCards);
        }

        List<WarriorStats> filteredCards = allCards.FindAll(card => card.rarity == rarity);
        return Rng.Entry(filteredCards);
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
        List<WarriorStats> warriors = allCards.Where(card => card.GetCost() == cost && card.cardType == CardType.Warrior).ToList();
        return Rng.Entry(warriors);
    }

    public static WarriorStats GetRandomWarriorWithSpecificRace(Character.Race race) {
        List<WarriorStats> warriors = allCards.Where(card => card.race == race).ToList();
        return Rng.Entry(warriors);
    }

    public static WarriorStats GetRandomSpell() {
        List<WarriorStats> warriors = allCards.Where(card => card.cardType == CardType.Spell).ToList();
        return Rng.Entry(warriors);
    }
}
