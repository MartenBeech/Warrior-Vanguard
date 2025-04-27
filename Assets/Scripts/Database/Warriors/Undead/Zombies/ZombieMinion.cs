public class ZombieMinion {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 1,
            strength = new int[] { 2, 3 },
            health = new int[] { 1, 1 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            classType = Character.ClassType.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.revive.Add();

        return stats;
    }
}