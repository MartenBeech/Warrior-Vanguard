public class PlagueWalker : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Plague Walker",
            attack = 2,
            health = 5,
            cost = 4,
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
        ability.revive.Add();
        ability.poison.Add(2);

        return stats;
    }
}