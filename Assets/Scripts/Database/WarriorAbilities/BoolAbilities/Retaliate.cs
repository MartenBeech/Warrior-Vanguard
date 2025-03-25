using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Retaliate {
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

    public async Task<bool> Trigger(Character dealer, Character target, GridManager gridManager) {
        if (GetValue(target.stats)) {
            int dist = gridManager.GetDistanceBetweenCharacters(dealer, target);
            if (dist > 0 && dist <= target.stats.range) {
                await target.Strike(dealer, target.stats.GetStrength());
                return true;
            }
        }
        return false;
    }

    public string GetTitle(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{GetAbilityName()}\n";
    }

    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"After I get attacked, I strike the attacker";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}