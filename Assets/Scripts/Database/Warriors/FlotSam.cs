public class FlotSam : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            strength = new int[] { 2, 2 },
            health = new int[] { 3, 3 },
            cost = new int[] { 2, 2 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.None,
            genre = Genre.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}