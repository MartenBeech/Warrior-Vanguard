public class CLASSNAMEWARRIOR : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "NAME",
            attack = 0,
            health = 0,
            cost = 0,
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

        WarriorAbility ability = stats.ability;

        return stats;
    }
}