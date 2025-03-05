public class Mario : WarriorStats {
    public static WarriorStats GetCard() {
        WarriorStats stats = new() {
            title = "Mario",
            attack = 0,
            health = 10,
            cost = 1,
            movementSpeed = 2,
            range = 2,
            numberOfAttacks = 1,
        };

        Bloodlust.Increase(stats, 1);

        return stats;
    }
}