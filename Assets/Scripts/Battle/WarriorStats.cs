public class WarriorStats {
    public string title = "";

    public int strength = 0;
    public int health = 0;
    public int healthMax = 0;
    public int cost = 0;
    public int speed = 2;
    public int range = 2;
    public int numberOfAttacks = 1;

    public int defaultAttack = 0;
    public int defaultHealth = 0;
    public int defaultCost = 0;
    public int defaultSpeed = 2;
    public int defaultRange = 2;
    public int defaultNumberOfAttacks = 1;

    public WarriorAbility ability = new();

    public void SetStats(WarriorStats stats) {
        title = stats.title;

        strength = stats.strength;
        health = stats.health;
        healthMax = stats.healthMax;
        cost = stats.cost;
        speed = stats.speed;
        range = stats.range;
        numberOfAttacks = stats.numberOfAttacks;

        defaultAttack = stats.defaultAttack;
        defaultHealth = stats.defaultHealth;
        defaultCost = stats.defaultCost;
        defaultSpeed = stats.defaultSpeed;
        defaultRange = stats.defaultRange;
        defaultNumberOfAttacks = stats.defaultNumberOfAttacks;

        ability = stats.ability;
    }

    public WarriorStats ResetStats() {
        strength = defaultAttack;
        health = defaultHealth;
        healthMax = defaultHealth;
        cost = defaultCost;
        speed = defaultSpeed;
        range = defaultRange;
        numberOfAttacks = defaultNumberOfAttacks;

        return this;
    }
}