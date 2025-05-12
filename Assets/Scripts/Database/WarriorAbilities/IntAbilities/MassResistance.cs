using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class MassResistance {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{WarriorAbility.Keywords.Summon}: Give all friends +{GetValue(stats)} Resistance";
    }

    public async Task<bool> Trigger(Character dealer, GridManager gridManager, FloatingText floatingText) {
        if (GetValue(dealer.stats) > 0) {
            List<Character> friends = gridManager.GetFriends(dealer.alignment);
            friends.Remove(dealer);
            List<Task> asyncFunctions = new();
            foreach (var friend in friends) {
                friend.stats.ability.resistance.Add(GetValue(dealer.stats));
                asyncFunctions.Add(floatingText.CreateFloatingText(friend.transform, $"+{GetValue(dealer.stats)} Resistance", ColorPalette.ColorEnum.teal));
            }
            await Task.WhenAll(asyncFunctions);

            return true;
        }
        return false;
    }

    int[] value = new int[] { 0, 0 };

    int GetValue(WarriorStats stats) {
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
}