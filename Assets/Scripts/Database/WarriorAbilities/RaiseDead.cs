using System.Text.RegularExpressions;
public class RaiseDead {
    bool[] value = new bool[] { false, false };

    bool GetValue(WarriorStats stats) {
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

    public bool Trigger(Character dealer, Character target, CharacterSpawner characterSpawner) {
        if (GetValue(dealer.stats)) {
            WarriorStats stats = new SkeletonWarrior().GetStats();
            stats.level = dealer.stats.level;

            characterSpawner.SpawnRandomly(stats, dealer.alignment, target.transform.position);
            return true;
        }
        return false;
    }

    public string GetTitle(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{GetAbilityName()}\n";
    }

    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Kill}: Summon a {new SkeletonWarrior().GetStats().strength[stats.level]}/{new SkeletonWarrior().GetStats().health[stats.level]} Skeleton Warrior";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}