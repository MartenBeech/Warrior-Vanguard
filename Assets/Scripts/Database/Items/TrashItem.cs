using System.Text.RegularExpressions;

public class TrashItem : Item {
    public Item GetItem() {
        Item item = new() {
            title = GetType().Name,
            description = "It is literally just some trash",
        };
        item.displayTitle = Regex.Replace(item.title, "(?<!^)([A-Z])", " $1");
        return item;
    }
}