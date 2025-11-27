using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class ForestFriend {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"When swapping, summon a bear with stats equal to this";
    }

    public async Task<bool> Trigger(Warrior dealer, WarriorSummoner warriorSummoner) {
        if (GetValue(dealer.stats)) {
            WarriorStats bear = new Bear().GetStats();
            bear.alignment = dealer.stats.alignment;
            bear.level = dealer.stats.level;
            bear.SetCost(dealer.stats.GetCost());
            bear.SetHealth(dealer.stats.GetHealthMax());
            bear.SetStrength(dealer.stats.GetStrength());

            await warriorSummoner.SummonRandomly(bear, dealer.transform.position);
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