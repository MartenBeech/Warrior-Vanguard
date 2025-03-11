using System.Text.RegularExpressions;
public class BloodMerchant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 5,
            strength = 3,
            health = 8,
            speed = 2,
            range = 4,
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
        ability.weaken.Add(2);

        return stats;
    }
}