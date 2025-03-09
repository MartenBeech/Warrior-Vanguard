public class Duck : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Duck",
            attack = 9001,
            health = 9001,
            cost = 1,
            speed = 2,
            range = 2,
            numberOfAttacks = 1,
        };
        stats.defaultAttack = stats.attack;
        stats.defaultHealth = stats.health;
        stats.defaultCost = stats.cost;
        stats.defaultSpeed = stats.speed;
        stats.defaultRange = stats.range;
        stats.defaultNumberOfAttacks = stats.numberOfAttacks;

        return stats;
    }
}