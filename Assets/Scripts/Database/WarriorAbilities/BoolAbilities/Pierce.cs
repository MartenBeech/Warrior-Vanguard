using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Pierce {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Attack}: Also strike the enemy behind the target";
    }

    public async Task<bool> Trigger(Character dealer, Character target, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            Character neighbor = gridManager.GetCharacterBehindTarget(target);

            if (neighbor && neighbor.stats.alignment != dealer.stats.alignment) {
                await dealer.Strike(neighbor);
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