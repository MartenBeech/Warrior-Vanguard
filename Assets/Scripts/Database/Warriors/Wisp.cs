public class Wisp : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            strength = new int[] { 1, 1 },
            health = new int[] { 1, 1 },
            cost = new int[] { 1, 1 },
            speed = 2,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.None,
            genre = Genre.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}