public class CardStats {
    public string title = "";
    public int attack = 0;
    public int health = 1;
    public int cost = 0;
    public int movementSpeed = 1;
    public int range = 1;
    public int numberOfAttacks = 1;

    public void SetStats(CardStats stats) {
        title = stats.title;
        attack = stats.attack;
        health = stats.health;
        cost = stats.cost;
        movementSpeed = stats.movementSpeed;
        range = stats.range;
        numberOfAttacks = stats.numberOfAttacks;
    }
}