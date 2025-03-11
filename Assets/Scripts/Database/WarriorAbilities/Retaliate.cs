using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Retaliate {
    bool value = false;

    public void Add() {
        value = true;
    }

    public void Remove() {
        value = false;
    }

    public async Task<bool> Trigger(Character dealer, Character target) {
        if (value) {
            await dealer.Strike(target);
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
        return $"When attacked, strike the attacker";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}