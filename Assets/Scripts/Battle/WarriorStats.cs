using System;
using System.Text.RegularExpressions;

public enum CardType {
    warrior,
    spell
}

public enum SpellTarget {
    none,
    friend,
    enemy,
    warrior,
}

public enum CardRarity {
    None,
    Common,
    Rare,
    Legendary
}

public class WarriorStats {
    public string title;
    public string displayTitle;
    public CardRarity rarity = CardRarity.Common;
    public int level = 0;
    public Character.DamageType damageType = Character.DamageType.Physical;
    public Character.Race race = Character.Race.None;
    public CharacterSpawner.Alignment alignment;
    public CardType cardType = CardType.warrior;
    public SpellTarget spellTarget = SpellTarget.none;
    public string[] spellDescription = new string[2];

    public int[] cost = new int[2];
    public int[] strength = new int[2];
    public int[] health = new int[2];
    public int[] healthMax = new int[2];
    public int speed = 2;
    public int range = 2;

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

        for (int i = 0; i < 2; i++) {
            cost[i] = stats.cost[i];
            strength[i] = stats.strength[i];
            health[i] = stats.health[i];
            healthMax[i] = stats.healthMax[i];
            spellDescription[i] = stats.spellDescription[i];
        }
        speed = stats.speed;
        range = stats.range;

        ability = stats.ability;
    }

    public void ResetStats() {
        Type type = Type.GetType(title);
        object instance = Activator.CreateInstance(type);
        WarriorStats defaultStats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);

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
        return cost[level];
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

    public void AddHealth(int amount) {
        for (int i = 0; i < 2; i++) {
            healthMax[i] += amount;
            health[i] += amount;
        }
    }
}