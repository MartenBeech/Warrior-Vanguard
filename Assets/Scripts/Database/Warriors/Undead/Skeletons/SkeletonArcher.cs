public class SkeletonArcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 2,
            strength = new int[] { 2, 3 },
            health = new int[] { 1, 2 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Physical,
            classType = Character.ClassType.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.skeletal.Add();

        return stats;
    }
}