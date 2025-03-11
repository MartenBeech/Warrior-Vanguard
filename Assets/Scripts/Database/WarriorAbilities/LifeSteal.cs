using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class LifeSteal {
    bool value = false;

    public void Add() {
        value = true;
    }

    public void Remove() {
        value = false;
    }

    public async Task<bool> Trigger(Character character, int damage) {
        if (value) {
            await character.Heal(character, damage);
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
        return $"{WarriorAbility.Keywords.Strike}: Heal equal to damage dealt";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}