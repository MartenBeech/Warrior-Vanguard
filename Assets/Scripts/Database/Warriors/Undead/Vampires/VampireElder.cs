using System.Text.RegularExpressions;
public class VampireElder {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 6,
            strength = 5,
            health = 9,
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
        ability.lifeSteal.Add();

        return stats;
    }
}