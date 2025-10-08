public class RadiantProtector {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 8, 8 },
            strength = new int[] { 3, 3 },
            health = new int[] { 10, 11 },
            speed = 4,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Unicorn,
            rarity = CardRarity.Legendary,
            genre = Genre.Elves
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.resistance.Add(1, 2);
        ability.massResistance.Add(1, 2);

        return stats;
    }
}