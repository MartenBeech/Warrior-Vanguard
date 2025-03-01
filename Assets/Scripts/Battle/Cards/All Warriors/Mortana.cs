public class Mortana : CardStats {
    public static CardStats GetCard() {
        CardStats stats = new() {
            title = "Mortana",
            attack = 11,
            health = 11,
            cost = 11
        };

        return stats;
    }
}