public class SmallHeart : Item {
    public SmallHeart() {
        title = "Small Heart";
        description = "All friendly warriors gain 1 health.";
    }

    public void Use(WarriorStats stats) {
        stats.AddHealth(1);
    }
}