public class WoodenSword : Item {
    public WoodenSword() {
        title = "Wooden Sword";
        description = "All friendly warriors gain 1 attack";
    }

    public override void UseOnWarriorSpawn(WarriorStats stats) {
        for (int i = 0; i < stats.strength.Length; i++) {
            stats.strength[i] += 1;
        }
    }
}