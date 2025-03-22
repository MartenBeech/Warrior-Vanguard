using System.Text.RegularExpressions;
public class VoidBeing {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 7,
            strength = new int[] { 5, 6 },
            health = new int[] { 4, 5 },
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
        ability.incorporeal.Add();
        ability.darkTouch.Add(5, 6);

        return stats;
    }
}