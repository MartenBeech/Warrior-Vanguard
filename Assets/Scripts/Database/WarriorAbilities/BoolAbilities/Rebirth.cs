using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Rebirth {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{Keyword.Overturn}: Turn into a new Phoenix";
    }

    public async Task<bool> TriggerOverturn(Warrior target, WarriorSummoner warriorSummoner) {
        if (GetValue(target.stats)) {
            WarriorStats phoenix = new Phoenix().GetStats();
            phoenix.alignment = target.stats.alignment;
            phoenix.level = target.stats.level;

            List<Task> asyncFunctions = new() {
                warriorSummoner.SummonRandomly(phoenix, target.transform.position),
                target.Die(target)
            };

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

    public BuffType buffType = BuffType.None;
}