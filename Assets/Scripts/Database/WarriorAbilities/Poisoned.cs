using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Poisoned {     // Update class name
    int value = 0;

    public void Add(int amount) {
        if (value < amount) {
            value = amount;
        }
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

    public async Task<bool> Trigger(Character character) {
        if (value > 0) {
            await character.TakeDamage(character, value);
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
        return $"{GetTitle()}End turn: Take {value} magic damage";  // Update description here
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}