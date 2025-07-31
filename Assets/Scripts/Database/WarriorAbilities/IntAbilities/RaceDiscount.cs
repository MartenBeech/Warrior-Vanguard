using System.Text.RegularExpressions;
using UnityEngine;
public class RaceDiscount {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{WarriorAbility.Keywords.Summon}: reduce the cost of every {Rng.GetRandomRace(Character.Genre.Underworld)} in your hand by {GetValue(stats)}. (Changes every game)";
    }

    public bool TriggerSummon(Character character, GridManager gridManager) {
        if (GetValue(character.stats) > 0) {
            gridManager.friendHand.ReduceCostRace(GetValue(character.stats), Rng.GetRandomRace(Character.Genre.Underworld));
            return true;
        }
        return false;
    }

    int[] value = new int[] { 0, 0 };

    public int GetValue(WarriorStats stats) {
        return value[stats.level];
    }

    public void Add(int unupgradedValue, int upgradedValue) {
        int[] newValues = new int[] { unupgradedValue, upgradedValue };
        for (int i = 0; i < 2; i++) {
            value[i] += newValues[i];
            if (value[i] < 0) {
                value[i] = 0;
            }
        }
    }

    public void Add(int value) {
        Add(value, value);
    }

    public void Remove() {
        for (int i = 0; i < 2; i++) {
            value[i] = 0;
        }
    }

    public string GetTitle(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{GetAbilityName()}: {GetValue(stats)}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}