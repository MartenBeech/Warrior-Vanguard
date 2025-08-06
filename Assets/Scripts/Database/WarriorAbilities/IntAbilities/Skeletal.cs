using System.Text.RegularExpressions;
public class Skeletal {
    public string GetDescription(WarriorStats stats) {
        if (GetValue(stats) == 0) return "";
        int value = GetValue(stats);
        return $"{WarriorAbility.Keywords.Death}: Gain {value} {(value > 1 ? "bones" : "bone")} to make future skeletons stronger";
    }

    public void TriggerDeath(Character target, Summoner summoner) {
        if (GetValue(target.stats) > 0) {
            summoner.stats.skeletonBones += GetValue(target.stats);
        }
    }

    public void TriggerSummon(Character target, Summoner summoner) {
        if (GetValue(target.stats) > 0) {

            for (int i = 0; i < summoner.stats.skeletonBones; i++) {
                int rng = Rng.Range(0, 7);
                if (rng < 4) {
                    target.stats.AddHealth(1);
                } else if (rng < 6) {
                    target.stats.AddStrength(1);
                } else {
                    switch (target.stats.title) {
                        case "SkeletonArcher":
                            target.stats.ability.poison.Add(1);
                            break;
                        case "SkeletonMage":
                            target.stats.ability.weaken.Add(1);
                            break;
                        case "SkeletonRider":
                            target.stats.ability.bloodlust.Add(1);
                            break;
                        case "SkeletonWarrior":
                            target.stats.ability.armor.Add(1);
                            break;
                        case "BoneDragon":
                            target.stats.ability.skeletal.Add(1);
                            break;
                        default:
                            target.stats.AddStrength(1);
                            break;
                    }
                }
            }
            target.UpdateWarriorUI();
        }
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

    public WarriorAbility.BuffType buffType = WarriorAbility.BuffType.None;
}