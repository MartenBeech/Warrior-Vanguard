public class Mario : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mario",
            strength = 2,
            health = 4,
            cost = 1,
            speed = 3,
            range = 3,
            numberOfAttacks = 1,
        };
        stats.healthMax = stats.health;
        stats.defaultAttack = stats.strength;
        stats.defaultHealth = stats.health;
        stats.defaultCost = stats.cost;
        stats.defaultSpeed = stats.speed;
        stats.defaultRange = stats.range;
        stats.defaultNumberOfAttacks = stats.numberOfAttacks;

        stats.ability.lifeSteal.Add();
        stats.ability.retaliate.Add();

        return stats;
    }
}