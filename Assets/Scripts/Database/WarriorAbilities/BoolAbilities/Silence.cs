using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class Silence {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Summon}: Remove all abilities from a random enemy";
    }

    public async Task<bool> TriggerSummon(Character dealer, GridManager gridManager, FloatingText floatingText) {
        if (GetValue(dealer.stats)) {
            List<Character> enemies = gridManager.GetEnemies(dealer.stats.alignment);
            Character randomEnemy = Rng.Entry(enemies);

            if (randomEnemy) {
                randomEnemy.stats.ability = new();
                randomEnemy.UpdateWarriorUI();
                await floatingText.CreateFloatingText(randomEnemy.transform, "Sileced", ColorPalette.ColorEnum.Purple);
            }
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