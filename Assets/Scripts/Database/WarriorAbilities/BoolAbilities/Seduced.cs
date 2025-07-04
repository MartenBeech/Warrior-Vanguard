using System.Text.RegularExpressions;
public class Seduced {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"Fight for your opponent next turn";
    }

    public bool Trigger(Character dealer) {
        if (GetValue(dealer.stats)) {
            dealer.stats.ability.seduced.Remove();
            dealer.stats.alignment = dealer.stats.alignment == CharacterSpawner.Alignment.Enemy ? CharacterSpawner.Alignment.Friend : CharacterSpawner.Alignment.Enemy;
            dealer.UpdateWarriorUI();
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