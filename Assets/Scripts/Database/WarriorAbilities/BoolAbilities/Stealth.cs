using System.Text.RegularExpressions;
using UnityEngine;
public class Stealth {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Take half damage. {WarriorAbility.Keywords.Strike}: Deal double damage and break stealth";
    }

    public int TriggerStrike(Character dealer, int damage) {
        if (GetValue(dealer.stats)) {
            damage *= 2;
            if (!dealer.stats.ability.permaStealth.GetValue(dealer.stats)) {
                dealer.stats.ability.stealth.Add(false, false);
            }
            dealer.UpdateWarriorUI();
        }
        return damage;
    }

    public int TriggerTakeDamage(Character target, int damage) {
        if (GetValue(target.stats)) {
            damage = (int)Mathf.Ceil(damage / 2f);
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