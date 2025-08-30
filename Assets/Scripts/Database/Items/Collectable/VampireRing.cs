public class VampireRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly vampires gain 2 health";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        if (parameters.stats.race == Character.Race.Vampire) {
            parameters.stats.AddHealth(2);
        }
    }
}