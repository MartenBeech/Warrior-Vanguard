public class HydraSerpent : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Hydra Serpent",
            attack = 2,
            health = 2,
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