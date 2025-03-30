public class SmallHeart : Item {
    public SmallHeart() {
        title = "Small Heart";
        description = "All friendly warriors gain 1 health.";
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        for (int i = 0; i < stats.health.Length; i++) {
            stats.health[i] += 1;
        }
    }
}