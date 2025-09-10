public class RejuvenatingOak {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 7, 7 },
            strength = new int[] { 0, 0 },
            health = new int[] { 12, 15 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Construct,
            rarity = CardRarity.None,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.lushGrounds.Add(3, 4);
        ability.forestProtection.Add(2, 3);

        return stats;
    }
}