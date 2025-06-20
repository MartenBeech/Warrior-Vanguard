public class Duck : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Duck",
            strength = new int[] { 9001, 9999 },
            health = new int[] { 9001, 9999 },
            cost = new int[] { 1, 1 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.None,
            genre = Character.Genre.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}