using System.Text.RegularExpressions;
public class Poison {
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

    public bool Trigger(Character dealer, Character target) {
        if (GetValue(dealer.stats) > 0) {
            target.stats.ability.poisoned.Add(GetValue(dealer.stats), GetValue(dealer.stats));
            target.UpdateWarriorUI();
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
        return $"{WarriorAbility.Keywords.Strike}: Apply {value} Poison";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}