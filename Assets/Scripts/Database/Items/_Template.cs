using System.Text.RegularExpressions;

public class CLASSNAME : Item {
    public Item GetItem() {
        Item item = new() {
            title = GetType().Name,
            description = "DESCRIPTION",
        };
        item.displayTitle = Regex.Replace(item.title, "(?<!^)([A-Z])", " $1");
        return item;
    }

    public override void UseImmediately() {
        GoldManager.AddGold(200);
    }
}