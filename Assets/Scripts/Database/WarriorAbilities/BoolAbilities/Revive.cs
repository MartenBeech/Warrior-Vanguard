using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Revive {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Death}: Resummon me without this ability";
    }

    public async Task<bool> Trigger(Character target, CharacterSpawner characterSpawner) {
        if (GetValue(target.stats)) {
            WarriorStats targetStats = new();
            targetStats.SetStats(target.stats);
            targetStats.ResetStats();
            targetStats.ability.revive.Remove();

            await characterSpawner.SpawnRandomly(targetStats, target.transform.position);
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