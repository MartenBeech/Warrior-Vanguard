using System.Text.RegularExpressions;

public class BigCoin : Item {
    public Item GetItem() {
        Item item = new() {
            title = GetType().Name,
            description = "Immediately gain 200 gold",
        };
        item.displayTitle = Regex.Replace(item.title, "(?<!^)([A-Z])", " $1");
        return item;
    }

    public override void UseImmediately() {
        GoldManager.AddGold(200);
    }
}