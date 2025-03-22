using System.Text.RegularExpressions;
public class BloodMerchant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 4,
            strength = new int[] { 3, 4 },
            health = new int[] { 8, 10 },
            speed = 2,
            range = 4,
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
        stats.rarity = CardRarity.Rare;

        WarriorAbility ability = stats.ability;
        ability.lifeSteal.Add();
        ability.weaken.Add(1, 1);

        return stats;
    }
}