using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Poisoned {
    int[] value = new int[] { 0, 0 };

    int GetValue(WarriorStats stats) {
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

    public void Remove() {
        for (int i = 0; i < 2; i++) {
            value[i] = 0;
        }
    }

    public async Task<bool> Trigger(Character target) {
        if (GetValue(target.stats) > 0) {
            await target.TakeDamage(GetValue(target.stats));
            return true;
        }
        return false;
    }

    public string GetTitle(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{GetAbilityName()}: {value}\n";
    }

    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{WarriorAbility.Keywords.Overturn}: Take {value} magic damage";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}