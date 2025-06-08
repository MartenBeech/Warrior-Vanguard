using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Backstab {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Can attack backwards, dealing double damage";
    }

    public async Task<bool> Trigger(Character dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            Character neighbor = gridManager.GetCharacterBehindTarget(dealer);

            if (neighbor && neighbor.alignment != dealer.alignment) {
                await dealer.Attack(neighbor, true);
                return true;
            }
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