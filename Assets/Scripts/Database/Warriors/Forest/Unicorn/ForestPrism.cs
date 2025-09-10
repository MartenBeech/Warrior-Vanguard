public class ForestPrism {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 5, 6 },
            health = new int[] { 9, 10 },
            speed = 4,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Unicorn,
            genre = Genre.Elves
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.resistance.Add(3, 5);

        return stats;
    }
}