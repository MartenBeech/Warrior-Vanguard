using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class DarkTouch {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{Keyword.Strike}: Instantly kill targets left with {GetValue(stats)} health or less";
    }

    public async Task<bool> TriggerStrike(Warrior dealer, Warrior target, FloatingText floatingText) {
        if (GetValue(dealer.stats) > 0) {
            if (target.stats.GetHealthCurrent() <= GetValue(dealer.stats)) {
                List<Task> asyncFunctions = new() {
                floatingText.CreateFloatingText(target.transform, "Dark Touch", ColorEnum.Red),
                target.Die(target)
            };
                await Task.WhenAll(asyncFunctions);
                return true;
            }
        }
        return false;
    }

    int[] value = new int[] { 0, 0 };

    public int GetValue(WarriorStats stats) {
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

    public void Add(int value) {
        Add(value, value);
    }

    public void Remove() {
        for (int i = 0; i < 2; i++) {
            value[i] = 0;
        }
    }

    public string GetTitle(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{GetAbilityName()}: {GetValue(stats)}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }

    public BuffType buffType = BuffType.None;
}