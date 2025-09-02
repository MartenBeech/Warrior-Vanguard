public class BigHeart : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain 2 health";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override void UseOnFriendSummon(ItemTriggerParams parameters) {
        parameters.stats.AddHealth(2);
    }
}