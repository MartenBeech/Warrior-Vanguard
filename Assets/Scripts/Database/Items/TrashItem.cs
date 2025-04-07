using System.Text.RegularExpressions;

public class TrashItem : Item {
    public Item GetItem() {
        title = GetType().Name;
        description = "It is literally just some trash";
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        return this;
    }
}