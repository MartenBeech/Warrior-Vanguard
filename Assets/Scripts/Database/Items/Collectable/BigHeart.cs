public class BigHeart : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 2 health";
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        parameters.stats.AddHealth(2);
    }
}