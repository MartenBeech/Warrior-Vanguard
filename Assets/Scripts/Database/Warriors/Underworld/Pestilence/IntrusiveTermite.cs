public class IntrusiveTermite {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            strength = new int[] { 1, 1 },
            health = new int[] { 2, 2 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Pestilence,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.spawn.Add(1, 2);

        return stats;
    }
}