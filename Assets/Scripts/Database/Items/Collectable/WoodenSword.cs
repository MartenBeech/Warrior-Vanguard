public class WoodenSword : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly warriors gain +1 strength";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override void UseOnFriendSummon(ItemTriggerParams parameters) {
        parameters.stats.AddStrength(1);
    }
}