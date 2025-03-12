public class Mario : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mario",
            strength = new int[] { 2, 2 },
            health = new int[] { 4, 4 },
            cost = 1,
            speed = 3,
            range = 3,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
            stats.defaultStrength[i] = stats.strength[i];
            stats.defaultHealth[i] = stats.health[i];
        }
        stats.defaultCost = stats.cost;
        stats.defaultSpeed = stats.speed;
        stats.defaultRange = stats.range;
        stats.defaultNumberOfAttacks = stats.numberOfAttacks;

        stats.ability.lifeSteal.Add();
        stats.ability.retaliate.Add();

        return stats;
    }
}