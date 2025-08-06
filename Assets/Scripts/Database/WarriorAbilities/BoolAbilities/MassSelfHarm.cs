using System.Collections.Generic;
using System.Text.RegularExpressions;
public class MassSelfHarm {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Aura}: Enemies have Self Harm";
    }

    public bool TriggerSummon(Character dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            List<Character> enemies = gridManager.GetEnemies(dealer.stats.alignment);
            foreach (var enemy in enemies) {
                enemy.stats.ability.selfHarm.Add();
                enemy.UpdateWarriorUI();
            }
            return true;
        }
        return false;
    }

    public bool TriggerDeath(Character dealer, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            List<Character> enemies = gridManager.GetEnemies(dealer.stats.alignment);
            foreach (var enemy in enemies) {
                enemy.stats.ability.selfHarm.Remove();
                enemy.UpdateWarriorUI();
            }
            return true;
        }
        return false;
    }

    public bool TriggerSummonEnemy(Character dealer, WarriorStats targetStats) {
        if (GetValue(dealer.stats)) {
            targetStats.ability.selfHarm.Add();
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

    public WarriorAbility.BuffType buffType = WarriorAbility.BuffType.None;
}