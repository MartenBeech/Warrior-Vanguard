public class ZombieHydra : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Zombie Hydra",
            attack = 6,
            health = 6,
            cost = 9,
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
        ability.hydraSplit.Add();
        ability.revive.Add();

        return stats;
    }
}