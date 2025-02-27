public class CardStats
{
    public string title = "";
    public int attack = 0;
    public int health = 0;
    public int cost = 0;

    public void SetStats(CardStats stats)
    {
        title = stats.title;
        attack = stats.attack;
        health = stats.health;
        cost = stats.cost;
    }
}