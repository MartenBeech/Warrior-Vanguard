public class Mario : WarriorStats {
    public static WarriorStats GetCard() {
        WarriorStats stats = new() {
            title = "Mario",
            attack = 2,
            health = 10,
            cost = 1,
            movementSpeed = 2,
            range = 2,
            numberOfAttacks = 1,
        };

        Bloodlust.Add(stats, 1);
        Revive.Add(stats);

        stats.healthMax = stats.health;

        return stats;
    }
}