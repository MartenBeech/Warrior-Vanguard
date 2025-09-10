public class UnstableExplosives {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 0, 0 },
            health = new int[] { 5, 7 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Construct,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.explosion.Add(5, 7);

        return stats;
    }
}