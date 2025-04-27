public class Mortana : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mortana",
            strength = new int[] { 11, 12 },
            health = new int[] { 11, 12 },
            cost = 11,
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            classType = Character.ClassType.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}