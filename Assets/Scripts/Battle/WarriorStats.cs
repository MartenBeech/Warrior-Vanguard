using System;
using System.Text.RegularExpressions;

public class WarriorStats {
    public string title;
    public string displayTitle;
    public CardRarity rarity = CardRarity.None;
    public int level = 0;
    public DamageType damageType = DamageType.Physical;
    public Race race = Race.None;
    public Genre genre = Genre.None;
    public Alignment alignment;
    public CardType cardType = CardType.Warrior;
    public SpellTarget spellTarget = SpellTarget.None;
    public string[] spellDescription = new string[2];

    public int[] cost = new int[2];
    public int[] strength = new int[2];
    public int[] health = new int[2];
    public int[] healthMax = new int[2];
    public int speed = 2;
    public int range = 2;

    public int tempStrength = 0;
    public bool attackedThisTurn = false;

    public WarriorAbility ability = new();

    public void SetStats(WarriorStats stats) {
        title = stats.title;
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        level = stats.level;
        damageType = stats.damageType;
        alignment = stats.alignment;
        cardType = stats.cardType;
        spellTarget = stats.spellTarget;
        race = stats.race;
        rarity = stats.rarity;
        genre = stats.genre;

        for (int i = 0; i < 2; i++) {
            cost[i] = stats.cost[i];
            strength[i] = stats.strength[i];
            health[i] = stats.health[i];
            healthMax[i] = stats.healthMax[i];
            spellDescription[i] = stats.spellDescription[i];
        }
        speed = stats.speed;
        range = stats.range;

        tempStrength = stats.tempStrength;
        ability.SetWarriorAbility(stats.ability);
    }

    public void ResetStats() {
        Type type = Type.GetType(title);
        object instance = Activator.CreateInstance(type);
        WarriorStats defaultStats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);

        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        strength = defaultStats.strength;
        for (int i = 0; i < 2; i++) {
            cost[i] = defaultStats.cost[i];
            strength[i] = defaultStats.strength[i];
            health[i] = defaultStats.health[i];
            healthMax[i] = defaultStats.healthMax[i];
        }
        speed = defaultStats.speed;
        range = defaultStats.range;
        ability = defaultStats.ability;
    }

    public int GetCost() {
        if (cost[level] < 0) {
            return 0;
        }
        return cost[level];
    }

    public void AddCost(int amount) {
        for (int i = 0; i < 2; i++) {
            cost[i] += amount;
        }
    }

    public int GetStrength() {
        int totalStrength = strength[level] - ability.weakened.GetValue(this) + tempStrength;
        if (totalStrength < 0) {
            totalStrength = 0;
        }
        return totalStrength;
    }

    public void AddStrength(int amount) {
        for (int i = 0; i < 2; i++) {
            strength[i] += amount;
            if (strength[i] < 0) {
                strength[i] = 0;
            }
        }
    }

    public void SetStrength(int amount) {
        for (int i = 0; i < 2; i++) {
            strength[i] = amount;
            if (strength[i] < 0) {
                strength[i] = 0;
            }
        }
    }

    public void AddHealth(int amount) {
        for (int i = 0; i < 2; i++) {
            healthMax[i] += amount;
            health[i] += amount;
        }
    }

    public void SetHealth(int amount) {
        for (int i = 0; i < 2; i++) {
            healthMax[i] = amount;
            health[i] = amount;
        }
    }

    public int GetHealthCurrent() {
        return health[level];
    }

    public void AddHealthCurrent(int amount) {
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
        }
    }

    public void SetHealthCurrent(int amount) {
        for (int i = 0; i < 2; i++) {
            health[i] = amount;
        }
    }

    public void DoubleStats() {
        for (int i = 0; i < 2; i++) {
            healthMax[i] *= 2;
            health[i] *= 2;
            strength[i] *= 2;
        }
    }
}