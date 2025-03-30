public class BigHeart : Item {
    public BigHeart() {
        title = "Big Heart";
        description = "All friendly warriors gain 2 health.";
    }

    public void Use(WarriorStats stats) {
        stats.AddHealth(2);
    }
}