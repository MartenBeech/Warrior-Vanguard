using System.Text.RegularExpressions;
public class Demolish {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Deals double damage to Constructs";
    }

    public int TriggerStrike(Warrior dealer, Warrior target, int damage) {
        if (GetValue(dealer.stats)) {
            if (target.stats.ability.construct.GetValue(target.stats)) {
                damage *= 2;
            }
            dealer.UpdateWarriorUI();
        }
        return damage;
    }

    bool[] value = new bool[] { false, false };

    public bool GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public void Add(bool unupgradedValue, bool upgradedValue) {
        bool[] newValues = new bool[] { unupgradedValue, upgradedValue };
        for (int i = 0; i < 2; i++) {
            value[i] = newValues[i];
        }
    }

    public void Add() {
        Add(true, true);
    }

    public void Remove() {
        Add(false, false);
    }

    public string GetTitle(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{GetAbilityName()}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }

    public BuffType buffType = BuffType.None;
}