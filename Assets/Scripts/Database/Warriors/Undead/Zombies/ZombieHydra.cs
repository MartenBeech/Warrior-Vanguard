using System.Text.RegularExpressions;
public class ZombieHydra {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 9,
            strength = 6,
            health = 6,
            speed = 2,
            range = 2,
            numberOfAttacks = 1,
        };
        stats.healthMax = stats.health;
        stats.defaultAttack = stats.strength;
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