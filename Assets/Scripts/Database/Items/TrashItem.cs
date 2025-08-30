public class TrashItem : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "It is literally just some trash";
        rarity = ItemManager.Rarity.None;
        return this;
    }
}