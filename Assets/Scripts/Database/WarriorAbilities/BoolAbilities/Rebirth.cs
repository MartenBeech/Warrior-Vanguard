using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Rebirth {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Overturn}: Turn into a resummoned Phoenix";
    }

    public async Task<bool> Trigger(Character target, CharacterSpawner characterSpawner) {
        if (GetValue(target.stats)) {
            WarriorStats phoenix = new Phoenix().GetStats();
            phoenix.alignment = target.alignment;

            List<Task> asyncFunctions = new() {
                characterSpawner.SpawnRandomly(phoenix, target.transform.position),
                target.Die(target)
            };

            await Task.WhenAll(asyncFunctions);
            return true;
        }
        return false;
    }

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