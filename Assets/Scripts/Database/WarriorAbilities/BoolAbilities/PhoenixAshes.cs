using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class PhoenixAshes {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Death}: Turn into 0/1 ashes that resummon this next turn";
    }

    public async Task<bool> TriggerDeath(Warrior target, WarriorSummoner warriorSummoner) {
        if (GetValue(target.stats)) {
            WarriorStats ashes = new Ashes().GetStats();
            ashes.alignment = target.stats.alignment;
            ashes.level = target.stats.level;

            await warriorSummoner.Summon(target.gridIndex, ashes, target.transform.position);
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