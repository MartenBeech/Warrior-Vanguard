using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Spawn {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        int value = GetValue(stats);
        return $"{WarriorAbility.Keywords.Summon}: Summon {value} extra {(value > 1 ? "copies" : "copy")} of this";
    }

    public async Task<bool> TriggerSummon(Character dealer, CharacterSpawner characterSpawner) {
        if (GetValue(dealer.stats) > 0) {

            List<Task> asyncFunctions = new();
            int value = GetValue(dealer.stats);

            for (int i = 0; i < value; i++) {
                WarriorStats clone = new();
                clone.SetStats(dealer.stats);
                clone.ability.spawn.Remove();
                asyncFunctions.Add(characterSpawner.SpawnRandomly(clone, dealer.transform.position));
            }

            await Task.WhenAll(asyncFunctions);
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