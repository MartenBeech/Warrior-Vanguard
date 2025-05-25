using System.Text.RegularExpressions;
public class Skeletal {
    public string GetDescription(WarriorStats stats) {
        if (!GetValue(stats)) return "";
        return $"{WarriorAbility.Keywords.Death}: Future skeletons are stronger";
    }

    public void TriggerDeath(Character target, GameManager gameManager) {
        if (GetValue(target.stats)) {
            Summoner summoner = target.stats.alignment == CharacterSpawner.Alignment.Enemy
                ? gameManager.enemySummonerObject.GetComponent<Summoner>()
                : gameManager.friendSummonerObject.GetComponent<Summoner>();

            summoner.stats.skeletonBones++;
        }
    }

    public void TriggerSummon(Character target, GameManager gameManager) {
        if (GetValue(target.stats)) {
            Summoner summoner = target.stats.alignment == CharacterSpawner.Alignment.Enemy
                ? gameManager.enemySummonerObject.GetComponent<Summoner>()
                : gameManager.friendSummonerObject.GetComponent<Summoner>();

            for (int i = 0; i < summoner.stats.skeletonBones; i++) {
                int rng = Rng.Range(0, 10);
                if (rng < 6) {
                    target.stats.AddHealthMax(1);
                } else if (rng < 9) {
                    target.stats.AddStrength(1);
                } else {
                    switch (target.stats.title) {
                        case "SkeletonArcher":
                            target.stats.range++;
                            break;
                        case "SkeletonMage":
                            target.stats.ability.poison.Add(1);
                            break;
                        case "SkeletonRider":
                            target.stats.speed++;
                            break;
                        case "SkeletonWarrior":
                            target.stats.ability.armor.Add(1);
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