public class Mortana : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mortana",
            attack = 11,
            health = 11,
            cost = 11,
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