public class Duck : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Duck",
            strength = new int[] { 9001, 9999 },
            health = new int[] { 9001, 9999 },
            cost = 1,
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            classType = Character.ClassType.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}