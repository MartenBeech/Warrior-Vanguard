using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class Whalecome {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Death}: Give the player amazing loot";
    }

    public void TriggerDeath(Warrior target) {
        if (GetValue(target.stats)) {
            ItemManager.RemoveItem(ItemManager.GetItemByTitle("WhaleHunt").GetItem());
            ItemManager.AddItem(ItemManager.GetItemByTitle("YouAreWhalecome").GetItem());
        }
    }

    bool[] value = new bool[] { false, false };

    public bool GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public bool GetValue(SummonerStats stats) {
        return value[0];
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

    public WarriorAbility.BuffType buffType = WarriorAbility.BuffType.None;
}