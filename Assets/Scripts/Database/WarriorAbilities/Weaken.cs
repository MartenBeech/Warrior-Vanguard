using System.Text.RegularExpressions;
public class Weaken {
    int value = 0;

    public void Add(int amount) {
        value += amount;
    }

    public void Remove() {
        value = 0;
    }

    public void Remove(int amount) {
        value -= amount;
        if (value < 0) {
            Remove();
        }
    }

    public bool Trigger(Character target) {
        if (value > 0) {
            target.stats.AddStrength(-value);
            target.UpdateWarriorUI();
            return true;
        }
        return false;
    }

    public string GetTitle() {
        if (value == 0) return "";
        return $"{GetAbilityName()}: {value}\n";
    }

    public string GetDescription() {
        if (value == 0) return "";
        return $"{WarriorAbility.Keywords.Attack}: Reduce the target's strength by {value}";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}