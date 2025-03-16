using System.Collections.Generic;
using System.Text.RegularExpressions;
public class Splash {
    bool[] value = new bool[] { false, false };

    bool GetValue(WarriorStats stats) {
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

    public bool Trigger(Character dealer, Character target, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            List<Character> characters = gridManager.GetWarriorsAroundCell(target.gridIndex);
            foreach (Character character in characters) {
                if (character.alignment != dealer.alignment) {
                    dealer.Strike(character, dealer.stats.GetStrength());
                }
            }
            return true;
        }
        return false;
    }

    public string GetTitle(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{GetAbilityName()}\n";
    }

    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Attack}: Also {WarriorAbility.Keywords.Strike} all enemies around your target";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}