using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
public class KnockBack {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{Keyword.Attack}: Push the target 2 tiles back";
    }

    public async Task<bool> TriggerAttack(Warrior dealer, Warrior target, GridManager gridManager) {
        if (GetValue(dealer.stats)) {
            for (int i = 2; i > 0; i--) {
                int xTilesToMove = target.stats.alignment == Alignment.Friend ? -i : i;
                Warrior obstacle = gridManager.GetCellWarrior(new Vector2(target.gridIndex.x + xTilesToMove, target.gridIndex.y));
                if (obstacle == null) {
                    await target.MoveWarrior(target.stats.alignment == Alignment.Friend ? Direction.Left : Direction.Right, i);
                    return true;
                }
            }
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