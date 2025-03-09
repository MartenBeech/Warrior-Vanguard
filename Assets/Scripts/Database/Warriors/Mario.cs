public class Mario : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mario",
            attack = 2,
            health = 4,
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

        stats.ability.bloodlust.Add(1);
        stats.ability.revive.Add();

        return stats;
    }
}