using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class BoneSpread {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Death}: Summon 3 random{(stats.level == 1 ? " upgraded" : "")} Skeletons";
    }
    public async Task<bool> TriggerDeath(Warrior target, WarriorSummoner warriorSummoner) {
        if (GetValue(target.stats)) {
            RaiseDead raiseDead = new();
            List<Task> asyncFunctions = new();
            for (int i = 0; i < 3; i++) {
                asyncFunctions.Add(raiseDead.SummonSkeleton(target, target, warriorSummoner));
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

    public WarriorAbility.BuffType buffType = WarriorAbility.BuffType.None;
}