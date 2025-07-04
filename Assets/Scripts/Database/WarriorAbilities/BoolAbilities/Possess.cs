using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Possess {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Kill}: Resummon the target on your side";
    }

    public async Task<bool> Trigger(Character dealer, Character target, CharacterSpawner characterSpawner) {
        if (GetValue(dealer.stats)) {
            WarriorStats targetStats = new();
            targetStats.SetStats(target.stats);
            targetStats.ResetStats();
            targetStats.alignment = dealer.stats.alignment;

            await characterSpawner.SpawnRandomly(targetStats, target.transform.position);

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