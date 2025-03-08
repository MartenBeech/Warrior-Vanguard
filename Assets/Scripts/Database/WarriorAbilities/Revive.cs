using System.Text.RegularExpressions;
public class Revive {
    bool value = false;

    public void Add() {
        value = true;
    }

    public void Remove() {
        value = false;
    }

    public bool Trigger(Character character) {
        if (value) {
            character.stats.health = character.stats.healthMax;
            Remove();
            character.UpdateWarriorUI();
            return true;
        }
        return false;
    }

    public string GetTitle() {
        if (!value) return "";
        return $"{GetAbilityName()}\n";
    }

    public string GetDescription() {
        if (!value) return "";
        return $"{GetTitle()}On death: resummon this minion without {GetAbilityName()}";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}