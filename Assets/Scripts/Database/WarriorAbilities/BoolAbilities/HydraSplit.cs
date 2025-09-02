using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class HydraSplit {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Death}: Summon 3 {new HydraSerpent().GetStats().strength[stats.level]}/{new HydraSerpent().GetStats().health[stats.level]} Hydra Serpents";
    }

    public async Task<bool> TriggerDeath(Warrior target, WarriorSummoner warriorSummoner) {
        if (GetValue(target.stats)) {
            List<Task> asyncFunctions = new();
            for (int i = 0; i < 3; i++) {
                WarriorStats stats = new HydraSerpent().GetStats();
                stats.level = target.stats.level;
                stats.alignment = target.stats.alignment;

                asyncFunctions.Add(warriorSummoner.SummonRandomly(stats, target.transform.position));
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