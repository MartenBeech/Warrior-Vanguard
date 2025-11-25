using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class LifeInDeath {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{Keyword.Kill}: Fully heal all other friends";
    }

    public async Task<bool> TriggerKill(Warrior dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            List<Warrior> damagedFriends = gridManager.GetDamagedFriends(dealer.stats.alignment);
            damagedFriends.Remove(dealer);

            List<Task> asyncFunctions = new();

            foreach (var friend in damagedFriends) {
                asyncFunctions.Add(friend.Heal(dealer, friend.stats.GetHealthMax() - friend.stats.GetHealthCurrent()));
            }

            await Task.WhenAll(asyncFunctions);
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