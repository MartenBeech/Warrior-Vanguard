public class CorpseBehemoth {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 7,
            strength = new int[] { 4, 6 },
            health = new int[] { 7, 7 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.revive.Add();
        ability.poison.Add(4, 6);
        ability.poisonCloud.Add(2, 3);

        return stats;
    }
}