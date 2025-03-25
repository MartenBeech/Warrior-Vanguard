public class Duck : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Duck",
            strength = new int[] { 9001, 9999 },
            health = new int[] { 9001, 9999 },
            cost = 1,
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
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