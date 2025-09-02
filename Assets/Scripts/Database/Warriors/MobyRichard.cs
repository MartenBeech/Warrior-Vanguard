public class MobyRichard : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            strength = new int[] { 10, 10 },
            health = new int[] { 10, 10 },
            cost = new int[] { 10, 10 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.None,
            genre = Warrior.Genre.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.whalecome.Add();

        return stats;
    }
}