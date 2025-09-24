using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
public class PoisoningAura {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"When attacked: Apply {GetValue(stats)} Poison to the attacker";
    }

    public async Task<bool> TriggerAttacked(Warrior dealer, Warrior target, FloatingText floatingText) {
        if (GetValue(target.stats) > 0) {
            dealer.stats.ability.poisoned.Add(GetValue(target.stats));
            dealer.UpdateWarriorUI();
            await floatingText.CreateFloatingText(dealer.transform, $"{GetValue(target.stats)}", ColorEnum.White, true, Resources.Load<Sprite>("Images/Icons/Poisoned"));
            return true;
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