public class Mortana : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mortana",
            strength = new int[] { 11, 12 },
            health = new int[] { 11, 12 },
            cost = 11,
            speed = 2,
            range = 2,
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

        return stats;
    }
}