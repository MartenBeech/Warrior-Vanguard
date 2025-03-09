public class CorpseBehemoth : WarriorStats {
    public static WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Corpse Behemoth",
            attack = 5,
            health = 7,
            cost = 7,
            speed = 2,
            range = 2,
            numberOfAttacks = 1,
        };
        stats.defaultAttack = stats.attack;
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