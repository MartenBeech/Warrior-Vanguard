using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Possess {
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

    public async Task<bool> Trigger(Character dealer, Character target, CharacterSpawner characterSpawner) {
        if (GetValue(dealer.stats)) {
            target.stats.ResetStats();
            target.stats.alignment = dealer.alignment;

            await characterSpawner.SpawnRandomly(target.stats, target.transform.position);

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
        return $"{WarriorAbility.Keywords.Kill}: Resummon the target on your side";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}