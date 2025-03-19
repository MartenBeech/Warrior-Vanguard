public class WarriorStats {
    public string title;
    public int level = 0;
    public Character.DamageType damageType = Character.DamageType.Physical;
    public CharacterSpawner.Alignment alignment;

    public int cost;
    public int[] strength = new int[2];
    public int[] health = new int[2];
    public int[] healthMax = new int[2];
    public int speed = 2;
    public int range = 2;
    public int numberOfAttacks = 1;

    public int defaultCost;
    public int[] defaultStrength = new int[2];
    public int[] defaultHealth = new int[2];
    public int defaultSpeed = 2;
    public int defaultRange = 2;
    public int defaultNumberOfAttacks = 1;

    public WarriorAbility ability = new();

    public void SetStats(WarriorStats stats) {
        title = stats.title;
        level = stats.level;
        damageType = stats.damageType;
        alignment = stats.alignment;

        for (int i = 0; i < 2; i++) {
            strength[i] = stats.strength[i];
            health[i] = stats.health[i];
            healthMax[i] = stats.healthMax[i];
        }
        cost = stats.cost;
        speed = stats.speed;
        range = stats.range;
        numberOfAttacks = stats.numberOfAttacks;

        defaultStrength = stats.defaultStrength;
        defaultHealth = stats.defaultHealth;
        defaultCost = stats.defaultCost;
        defaultSpeed = stats.defaultSpeed;
        defaultRange = stats.defaultRange;
        defaultNumberOfAttacks = stats.defaultNumberOfAttacks;

        ability = stats.ability;
    }

    public void ResetStats() {
        strength = defaultStrength;
        for (int i = 0; i < 2; i++) {
            health[i] = defaultHealth[i];
            healthMax[i] = defaultHealth[i];
        }
        cost = defaultCost;
        speed = defaultSpeed;
        range = defaultRange;
        numberOfAttacks = defaultNumberOfAttacks;
    }

    public int GetStrength() {
        return strength[level];
    }

    public void AddStrength(int amount) {
        for (int i = 0; i < 2; i++) {
            strength[i] += amount;
            if (strength[i] < 0) {
                strength[i] = 0;
            }
        }
    }

    public int GetHealth() {
        return health[level];
    }

    public void AddHealth(int amount) {
        for (int i = 0; i < 2; i++) {
            health[i] += amount;
            if (health[i] > healthMax[i]) {
                health[i] = healthMax[i];
            }
        }
    }

    public int GetHealthMax() {
        return healthMax[level];
    }

    public void AddHealthMax(int amount) {
        for (int i = 0; i < 2; i++) {
            healthMax[i] += amount;
            health[i] += amount;
        }
    }
}