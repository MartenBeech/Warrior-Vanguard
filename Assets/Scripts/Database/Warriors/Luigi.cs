public class Luigi : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Green Mario",
            strength = 2,
            health = 10,
            cost = 1,
            speed = 2,
            range = 2,
            numberOfAttacks = 1,
        };
        stats.defaultAttack = stats.strength;
        stats.defaultHealth = stats.health;
        stats.defaultCost = stats.cost;
        stats.defaultSpeed = stats.speed;
        stats.defaultRange = stats.range;
        stats.defaultNumberOfAttacks = stats.numberOfAttacks;

        return stats;
    }
}