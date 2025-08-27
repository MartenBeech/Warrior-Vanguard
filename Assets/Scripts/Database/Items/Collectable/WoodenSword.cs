public class WoodenSword : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain +1 strength";
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        parameters.stats.AddStrength(1);
    }
}