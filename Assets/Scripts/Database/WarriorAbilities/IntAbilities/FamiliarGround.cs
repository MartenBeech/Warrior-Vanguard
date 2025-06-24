using System.Text.RegularExpressions;
using UnityEngine;
public class FamiliarGround {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"Has +{GetValue(stats)}/+{GetValue(stats)} while inside your deployment area";
    }

    public bool TriggerSummon(Character dealer) {
        if (GetValue(dealer.stats) > 0) {
            dealer.stats.AddStrength(GetValue(dealer.stats));
            dealer.stats.AddHealth(GetValue(dealer.stats));
            dealer.UpdateWarriorUI();
            return true;
        }
        return false;
    }

    public bool TriggerMove(Character dealer, Vector2 moveFrom, Vector2 moveTo, GridManager gridManager) {
        if (GetValue(dealer.stats) > 0) {
            if (dealer.stats.alignment == CharacterSpawner.Alignment.Friend) {
                if (moveFrom.x < 3 && moveTo.x >= 3) {
                    dealer.stats.AddStrength(-GetValue(dealer.stats));
                    dealer.stats.AddHealth(-GetValue(dealer.stats));
                } else if (moveFrom.x >= 3 && moveTo.x < 3) {
                    dealer.stats.AddStrength(GetValue(dealer.stats));
                    dealer.stats.AddHealth(GetValue(dealer.stats));
                }
            }

            if (dealer.stats.alignment == CharacterSpawner.Alignment.Enemy) {
                if (moveFrom.x >= gridManager.columns - 3 && moveTo.x < gridManager.columns - 3) {
                    dealer.stats.AddStrength(-GetValue(dealer.stats));
                    dealer.stats.AddHealth(-GetValue(dealer.stats));
                } else if (moveFrom.x < gridManager.columns - 3 && moveTo.x >= gridManager.columns - 3) {
                    dealer.stats.AddStrength(GetValue(dealer.stats));
                    dealer.stats.AddHealth(GetValue(dealer.stats));
                }
            }

            dealer.UpdateWarriorUI();
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