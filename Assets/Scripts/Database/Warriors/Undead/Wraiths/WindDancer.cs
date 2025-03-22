using System.Text.RegularExpressions;
public class WindDancer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 1,
            strength = new int[] { 2, 3 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 2,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
            stats.defaultStrength[i] = stats.strength[i];
            stats.defaultHealth[i] = stats.health[i];
        }
        stats.defaultCost = stats.cost;
        stats.defaultSpeed = stats.speed;
        stats.defaultRange = stats.range;
        stats.defaultNumberOfAttacks = stats.numberOfAttacks;
        stats.rarity = CardRarity.Legendary;

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();

        return stats;
    }
}