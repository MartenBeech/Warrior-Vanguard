public class Mario : CardStats {
    public static CardStats GetCard() {
        CardStats stats = new() {
            title = "Mario",
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