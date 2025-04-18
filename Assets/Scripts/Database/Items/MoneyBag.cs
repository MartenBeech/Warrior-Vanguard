using System.Text.RegularExpressions;

public class MoneyBag : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Immediately gain 200 gold";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }

    public override void UseImmediately() {
        GoldManager.AddGold(200);
    }
}