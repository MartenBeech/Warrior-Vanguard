public class SmallHeart : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 1 health";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        stats.AddHealth(1);
    }
}