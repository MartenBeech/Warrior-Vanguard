public class BoneGnawer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 1,
            strength = new int[] { 2, 2 },
            health = new int[] { 3, 3 },
            speed = 3,
            range = 2,
            damageType = Character.DamageType.Physical,
            classType = Character.ClassType.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cannibalism.Add(1, 2);

        return stats;
    }
}