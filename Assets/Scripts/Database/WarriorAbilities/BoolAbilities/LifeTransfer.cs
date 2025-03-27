using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
public class LifeTransfer {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Strike}: Heal another damaged friend equal to damage dealt";
    }

    public async Task<bool> Trigger(Character dealer, int damage, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            List<Character> friends = gridManager.GetFriends(dealer.alignment);
            friends.Remove(dealer);
            List<Character> damagedfriends = friends.Where(friend => friend.stats.GetHealth() < friend.stats.GetHealthMax()).ToList();
            if (damagedfriends.Count == 0) return false;

            Character randomDamagedFriend = damagedfriends[Rng.Range(0, damagedfriends.Count)];
            await randomDamagedFriend.Heal(dealer, damage);
            return true;
        }
        return false;
    }

    bool[] value = new bool[] { false, false };

    bool GetValue(WarriorStats stats) {
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
}