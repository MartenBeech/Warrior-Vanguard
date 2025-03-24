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
            SummonSkeleton(dealer, target, characterSpawner);
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
        return $"{WarriorAbility.Keywords.Kill}: Summon a random{(stats.level == 1 ? " upgraded" : "")} Skeleton";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }

    public void SummonSkeleton(Character dealer, Character target, CharacterSpawner characterSpawner) {
        int skeletonType = Rng.Range(0, 3);

        WarriorStats stats =
            skeletonType == 1 ? new SkeletonWarrior().GetStats() :
            skeletonType == 2 ? new SkeletonArcher().GetStats() :
            new SkeletonMage().GetStats();
        stats.level = dealer.stats.level;

        characterSpawner.SpawnRandomly(stats, dealer.alignment, target.transform.position);
    }
}