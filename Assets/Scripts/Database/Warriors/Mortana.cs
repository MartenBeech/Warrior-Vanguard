public class Mortana : CardStats {
    public static CardStats GetCard() {
        CardStats stats = new() {
            title = "Mortana",
            attack = 11,
            health = 11,
            cost = 11,
            movementSpeed = 2,
            range = 2,
            numberOfAttacks = 1,
        };

        return stats;
    }
}