using System.Collections.Generic;
using System.Text.RegularExpressions;
public class MassImmolate {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"{WarriorAbility.Keywords.Aura}: Friends have +{GetValue(stats)} Immolate";
    }

    public bool TriggerSummon(Character dealer, GridManager gridManager) {
        if (GetValue(dealer.stats) > 0) {
            List<Character> friends = gridManager.GetFriends(dealer.stats.alignment);
            friends.Remove(dealer);
            foreach (var friend in friends) {
                friend.stats.ability.immolate.Add(GetValue(dealer.stats));
                friend.UpdateWarriorUI();
            }
            return true;
        }
        return false;
    }

    public bool TriggerDeath(Character dealer, GridManager gridManager) {
        if (GetValue(dealer.stats) > 0) {
            List<Character> friends = gridManager.GetFriends(dealer.stats.alignment);
            friends.Remove(dealer);
            foreach (var friend in friends) {
                friend.stats.ability.immolate.Add(-GetValue(dealer.stats));
                friend.UpdateWarriorUI();
            }
            return true;
        }
        return false;
    }

    public bool TriggerSummonFriend(Character dealer, WarriorStats targetStats) {
        if (GetValue(dealer.stats) > 0) {
            targetStats.ability.immolate.Add(GetValue(dealer.stats));
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
}