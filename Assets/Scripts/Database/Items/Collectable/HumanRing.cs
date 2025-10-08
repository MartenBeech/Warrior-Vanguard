public class HumanRing : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "All friendly humans gain 2 health";
        rarity = ItemRarity.Normal;
        genre = Genre.Human;
        return this;
    }

    public override void UseOnFriendSummon(ItemTriggerParams parameters) {
        if (parameters.stats.genre == Genre.Human) {
            parameters.stats.AddHealth(2);
        }
    }
}