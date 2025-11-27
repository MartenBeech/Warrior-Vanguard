using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
public class TurnSwap {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{Keyword.Overturn}: Swap place with the warrior furthest behind this";
    }

    public async Task<bool> TriggerOverturn(Warrior dealer, GridManager gridManager, WarriorSummoner warriorSummoner) {
        if (GetValue(dealer.stats)) {
            Warrior target = null;
            if (dealer.stats.alignment == Alignment.Friend) {
                for (int x = 0; x < dealer.gridIndex.x; x++) {
                    target = gridManager.GetCellWarrior(new Vector2(x, dealer.gridIndex.y));
                    if (target) break;
                }
            } else if (dealer.stats.alignment == Alignment.Enemy) {
                for (int x = GridManager.columns; x > dealer.gridIndex.x; x--) {
                    target = gridManager.GetCellWarrior(new Vector2(x, dealer.gridIndex.y));
                    if (target) break;
                }
            }
            if (!target) return false;

            int xDist = (int)Mathf.Abs(dealer.gridIndex.x - target.gridIndex.x);

            List<Task> asyncFunctions = new() {
                dealer.MoveWarrior(dealer.stats.alignment == Alignment.Friend ? Direction.Left : Direction.Right, xDist, 1),
                target.MoveWarrior(dealer.stats.alignment == Alignment.Friend ? Direction.Right : Direction.Left, xDist, 1),
                dealer.stats.ability.forestFriend.Trigger(dealer, warriorSummoner)
            };
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