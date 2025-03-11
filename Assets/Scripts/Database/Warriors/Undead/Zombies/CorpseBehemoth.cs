using System.Text.RegularExpressions;
public class CorpseBehemoth {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 7,
            strength = 5,
            health = 7,
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
        ability.revive.Add();
        ability.poison.Add(5);

        return stats;
    }
}