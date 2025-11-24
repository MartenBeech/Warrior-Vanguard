using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Reckoning {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{Keyword.Overturn}: Instantly kill enemies with {GetValue(stats)} health or less";
    }

    public async Task<bool> TriggerOverturn(Warrior dealer, GridManager gridManager, FloatingText floatingText) {
        if (GetValue(dealer.stats) > 0) {
            List<Warrior> enemies = gridManager.GetEnemies(dealer.stats.alignment);
            List<Task> asyncFunctions = new();
            foreach (Warrior enemy in enemies) {
                if (enemy.stats.GetHealthCurrent() <= GetValue(dealer.stats)) {
                    asyncFunctions.Add(enemy.Die(dealer));
                    asyncFunctions.Add(floatingText.CreateFloatingText(enemy.transform, "Reckoning", ColorEnum.Red));
                }
            }
            await Task.WhenAll(asyncFunctions);
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