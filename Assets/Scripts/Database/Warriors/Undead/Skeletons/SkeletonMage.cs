using System.Text.RegularExpressions;
public class SkeletonMage {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 2,
            strength = new int[] { 2, 3 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical
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

        WarriorAbility ability = stats.ability;
        ability.skeletal.Add();
        ability.weaken.Add(1);

        return stats;
    }
}