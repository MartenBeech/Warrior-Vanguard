using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Bash {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{Keyword.Strike}: 50% chance to stun the target";
    }

    public async Task<bool> TriggerStrike(Warrior dealer, Warrior target, FloatingText floatingText) {
        if (GetValue(dealer.stats)) {
            if (Rng.Chance(50)) {
                target.stats.ability.stunned.Add();
                target.UpdateWarriorUI();
                await floatingText.CreateFloatingText(target.transform, "Stunned", ColorPalette.ColorEnum.Purple);
            }
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

    public BuffType buffType = BuffType.None;
}