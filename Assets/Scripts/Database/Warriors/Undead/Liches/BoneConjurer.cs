using System.Text.RegularExpressions;
public class BoneConjurer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 5,
            strength = new int[] { 3, 4 },
            health = new int[] { 7, 8 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
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
        ability.raiseDead.Add();
        ability.boneSculptor.Add(3, 4);

        return stats;
    }
}