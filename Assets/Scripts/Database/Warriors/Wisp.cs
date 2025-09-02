public class Wisp : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            strength = new int[] { 1, 1 },
            health = new int[] { 1, 1 },
            cost = new int[] { 1, 1 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Magical,
            race = Warrior.Race.None,
            genre = Warrior.Genre.None,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}