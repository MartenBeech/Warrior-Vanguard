public class MoneyBag : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Immediately gain 200 gold";
        rarity = ItemRarity.Normal;
        genre = Genre.None;
        return this;
    }

    public override void UseImmediately(ItemTriggerParams parameters) {
        GoldManager.AddGold(200);
    }
}