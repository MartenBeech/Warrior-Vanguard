public class Wisp : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Wisp",
            strength = new int[] { 1, 1 },
            health = new int[] { 1, 1 },
            cost = new int[] { 1, 1 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Magical,
            race = Character.Race.None,
            genre = Character.Genre.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}