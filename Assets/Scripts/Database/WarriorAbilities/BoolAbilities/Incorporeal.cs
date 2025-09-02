using System.Text.RegularExpressions;
public class Incorporeal {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Can max take 1 physical damage at a time";
    }

    public int TriggerDamaged(Warrior target, int damage, DamageType damageType) {
        if (GetValue(target.stats)) {
            if (damageType == DamageType.Physical) {
                if (damage > 1) {
                    damage = 1;
                }
            }
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