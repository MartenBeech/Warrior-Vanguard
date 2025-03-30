public class BigHeart : Item {
    public BigHeart() {
        title = "Big Heart";
        description = "All friendly warriors gain 2 health.";
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        for (int i = 0; i < stats.health.Length; i++) {
            stats.health[i] += 2;
        }
    }
}