using System.Text.RegularExpressions;
public class ShadowyEntity {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = Regex.Replace(GetType().Name, "(?<!^)([A-Z])", " $1"),
            cost = 10,
            strength = new int[] { 4, 5 },
            health = new int[] { 4, 5 },
            speed = 3,
            range = 2,
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
        ability.cannibalism.Add(4, 5);
        ability.stealth.Add();
        ability.permaStealth.Add();

        return stats;
    }
}