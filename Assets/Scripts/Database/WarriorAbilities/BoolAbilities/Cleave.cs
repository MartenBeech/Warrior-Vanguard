using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Cleave {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Attack}: Hit all enemies in the column";
    }

    public async Task<bool> Trigger(Character dealer, Character target, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            List<Character> enemies = gridManager.GetFriendsOnColumn(target);
            List<Task> asyncFunctions = new();
            foreach (Character enemy in enemies) {
                asyncFunctions.Add(dealer.Strike(enemy));
            }

            await Task.WhenAll(asyncFunctions);
            return true;
        }
        return false;
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