public class Damnation {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 1, 1 },
            health = new int[] { 2, 2 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Pestilence,
            rarity = CardRarity.Legendary,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.spawn.Add(3, 4);
        ability.explosion.Add(3, 4);

        return stats;
    }
}