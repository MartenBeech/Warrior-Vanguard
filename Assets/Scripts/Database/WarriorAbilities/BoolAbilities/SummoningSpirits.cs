using System.Collections.Generic;
using System.Text.RegularExpressions;
public class SummoningSpirits {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Your cards cost 1 less";
    }

    public bool Trigger(Character character) {
        if (GetValue(character.stats)) {
            // Add trigger event here
            character.UpdateWarriorUI();
            return true;
        }
        return false;
    }

    public bool TriggerSummon(Character character, GridManager gridManager) {
        if (GetValue(character.stats)) {
            gridManager.friendHand.ReduceCostAllCards(1);
            return true;
        }
        return false;
    }

    public bool TriggerDeath(Character character, GridManager gridManager) {
        if (GetValue(character.stats)) {
            gridManager.friendHand.IncreaseCostAllCards(1);
            return true;
        }
        return false;
    }

    public void TriggerDrawCard(WarriorStats stats) {
            stats.ReduceCost(1);
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