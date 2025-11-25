using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
public class MassBuilder {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{Keyword.Overturn}: Summon a Cannon Tower in front of this";
    }

    public async Task<bool> TriggerOverturn(Warrior dealer, GridManager gridManager, WarriorSummoner warriorSummoner) {
        if (GetValue(dealer.stats)) {
            int xDirection = dealer.stats.alignment == Alignment.Friend ? 1 : -1;
            Vector2 gridToSummon = new(dealer.gridIndex.x + xDirection, dealer.gridIndex.y);

            if (gridManager.GetCellWarrior(gridToSummon)) return false;

            WarriorStats cannonTower = new CannonTower().GetStats();
            cannonTower.alignment = dealer.stats.alignment;
            cannonTower.level = dealer.stats.level;
            await warriorSummoner.Summon(gridToSummon, cannonTower, dealer.transform.position);
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