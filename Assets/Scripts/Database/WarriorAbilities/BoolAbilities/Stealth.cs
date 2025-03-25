using System.Text.RegularExpressions;
public class Stealth {
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

    public bool TriggerAttack(Character dealer) {
        if (GetValue(dealer.stats)) {
            if (!dealer.stats.ability.permaStealth.GetValue(dealer.stats)) {
                dealer.stats.ability.stealth.Add(false, false);
            }
            dealer.UpdateWarriorUI();
            return true;
        }
        return false;
    }

    public bool TriggerTakeDamage(Character target) {
        if (GetValue(target.stats)) {
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
        return $"Take half damage. {WarriorAbility.Keywords.Attack}: Deal double damage and break stealth";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}