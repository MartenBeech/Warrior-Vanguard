
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
public class Afterlife {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Death}: Return to your hand without this ability";
    }

    public async Task<bool> Trigger(Character target, GridManager gridManager, Hand hand, Transform summonerObject, GameObject clone) {
        if (GetValue(target.stats)) {
            ObjectAnimation objectAnimation = clone.GetComponent<ObjectAnimation>();
            await objectAnimation.MoveObject(gridManager.GetCellPosition(target.gridIndex), summonerObject.position, 1, true);

            target.stats.ResetStats();
            target.stats.ability.afterlife.Remove();
            hand.AddCardToHand(target.stats);
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
}