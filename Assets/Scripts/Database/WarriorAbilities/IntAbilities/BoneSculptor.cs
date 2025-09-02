using System.Text.RegularExpressions;
public class BoneSculptor {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        return $"When you summon a Skeleton, give it +{GetValue(stats)}/+{GetValue(stats)}";
    }

    public bool TriggerSummonFriend(Warrior dealer, WarriorStats targetStats) {
        if (GetValue(dealer.stats) > 0) {
            if (targetStats.ability.skeletal.GetValue(targetStats) > 0) {
                targetStats.AddStrength(GetValue(dealer.stats));
                targetStats.AddHealth(GetValue(dealer.stats));
            }
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

    public BuffType buffType = BuffType.None;
}