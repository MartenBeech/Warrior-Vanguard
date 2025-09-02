public class VampireRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly vampires gain 2 health";
        rarity = ItemRarity.Normal;
        return this;
    }

    public override void UseOnFriendSummon(ItemTriggerParams parameters) {
        if (parameters.stats.race == Race.Vampire) {
            parameters.stats.AddHealth(2);
        }
    }
}