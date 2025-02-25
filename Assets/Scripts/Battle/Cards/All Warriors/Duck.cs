public class Duck : CardStats
{
    public static CardStats GetCard()
    {
        CardStats stats = new()
        {
            title = "Duck",
            attack = 9001,
            health = 9001,
            cost = 1
        };

        return stats;
    }
}