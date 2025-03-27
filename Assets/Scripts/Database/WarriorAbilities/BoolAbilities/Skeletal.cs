using System.Text.RegularExpressions;
using UnityEngine;
public class Skeletal {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Take half damage from attackers with 4+ range";
    }

    public int Trigger(Character dealer, Character target, int damage) {
        if (GetValue(target.stats)) {
            if (dealer.stats.range >= 4) {
                damage = (int)Mathf.Ceil(damage / 2f);
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
}