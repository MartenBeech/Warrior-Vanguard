public class HumanRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly humans gain 2 health";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        if (parameters.stats.race == Character.Race.Human) {
            parameters.stats.AddHealth(2);
        }
    }
}