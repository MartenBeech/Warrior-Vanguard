using System.Collections.Generic;
using System.Text.RegularExpressions;
public class ForestStrength {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{WarriorAbility.Keywords.Aura}: Your other warriors have +{GetValue(stats)} strength";
    }

    public bool TriggerSummon(Warrior dealer, GridManager gridManager) {
        if (GetValue(dealer.stats) > 0) {
            List<Warrior> friends = gridManager.GetFriends(dealer.stats.alignment);
            friends.Remove(dealer);
            foreach (var friend in friends) {
                friend.stats.AddStrength(GetValue(dealer.stats));
                friend.UpdateWarriorUI();
            }
            return true;
        }
        return false;
    }

    public bool TriggerDeath(Warrior dealer, GridManager gridManager) {
        if (GetValue(dealer.stats) > 0) {
            List<Warrior> friends = gridManager.GetFriends(dealer.stats.alignment);
            friends.Remove(dealer);
            foreach (var friend in friends) {
                friend.stats.AddStrength(-GetValue(dealer.stats));
                friend.UpdateWarriorUI();
            }
            return true;
        }
        return false;
    }

    public bool TriggerSummonFriend(Warrior dealer, WarriorStats targetStats) {
        if (GetValue(dealer.stats) > 0) {
            targetStats.AddStrength(GetValue(dealer.stats));
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

    public WarriorAbility.BuffType buffType = WarriorAbility.BuffType.None;
}