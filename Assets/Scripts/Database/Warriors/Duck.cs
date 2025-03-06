public class Duck : WarriorStats {
    public static WarriorStats GetCard() {
        WarriorStats stats = new() {
            title = "Duck",
            attack = 9001,
            health = 9001,
            cost = 1,
            movementSpeed = 2,
            range = 2,
            numberOfAttacks = 1,
        };

        stats.healthMax = stats.health;

        return stats;
    }
}