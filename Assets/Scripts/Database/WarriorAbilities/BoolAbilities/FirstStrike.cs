using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class FirstStrike {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Before getting attacked, strike the attacker";
    }

    public async Task<bool> TriggerAttacked(Warrior dealer, Warrior target, GridManager gridManager) {
        if (GetValue(target.stats)) {
            int dist = gridManager.GetDistanceBetweenWarriors(dealer, target);
            if (dist > 0 && dist <= target.stats.range) {
                await target.Strike(dealer);
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

    public BuffType buffType = BuffType.None;
}