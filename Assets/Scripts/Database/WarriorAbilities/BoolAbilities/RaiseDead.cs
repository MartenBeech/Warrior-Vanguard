using System.Text.RegularExpressions;
using System.Threading.Tasks;
public class RaiseDead {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Kill}: Summon a random{(stats.level == 1 ? " upgraded" : "")} Skeleton";
    }

    public async Task<bool> Trigger(Character dealer, Character target, CharacterSpawner characterSpawner) {
        if (GetValue(dealer.stats)) {
            await SummonSkeleton(dealer, target, characterSpawner);
            return true;
        }
        return false;
    }

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

    public string GetTitle(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{GetAbilityName()}\n";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }

    public async Task SummonSkeleton(Character dealer, Character target, CharacterSpawner characterSpawner, CharacterSpawner.Alignment alignment = CharacterSpawner.Alignment.Null) {
        int skeletonType = Rng.Range(0, 4);

        WarriorStats stats =
            skeletonType == 1 ? new SkeletonWarrior().GetStats() :
            skeletonType == 2 ? new SkeletonArcher().GetStats() :
            skeletonType == 3 ? new SkeletonMage().GetStats() :
            new SkeletonRider().GetStats();
        stats.level = dealer.stats.level;
        stats.alignment = alignment == CharacterSpawner.Alignment.Null ? dealer.stats.alignment : alignment;

        await characterSpawner.SpawnRandomly(stats, target.transform.position);
    }
}