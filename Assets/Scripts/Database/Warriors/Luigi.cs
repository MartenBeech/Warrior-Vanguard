public class Luigi : WarriorStats {
    public static WarriorStats GetCard() {
        WarriorStats stats = new() {
            title = "Green Mario",
            attack = 2,
            health = 10,
            cost = 1,
            movementSpeed = 2,
            range = 2,
            numberOfAttacks = 1,
        };

        return stats;
    }
}