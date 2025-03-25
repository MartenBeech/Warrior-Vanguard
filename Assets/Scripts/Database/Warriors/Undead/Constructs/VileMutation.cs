using System.Text.RegularExpressions;
public class VileMutation {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 7,
            strength = new int[] { 0, 0 },
            health = new int[] { 9, 11 },
            speed = 0,
            range = 0,
            damageType = Character.DamageType.Physical,
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
        ability.construct.Add();
        ability.poisonCloud.Add(3, 4);
        ability.poisoningAura.Add(3, 4);

        return stats;
    }
}