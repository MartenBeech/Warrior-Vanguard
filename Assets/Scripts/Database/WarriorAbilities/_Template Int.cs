using System.Text.RegularExpressions;
public class CLASSNAMEINT {     // Update class name
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

    public bool Trigger(Character character) {
        if (value > 0) {
            // Add trigger event here
            character.UpdateWarriorUI();
            return true;
        }
        return false;
    }

    public string GetTitle() {
        if (value == 0) return "";
        return $"{GetAbilityName()} {value}\n";
    }

    public string GetDescription() {
        if (value == 0) return "";
        return $"{GetTitle()}DESCRIPTION";  // Update description here
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}