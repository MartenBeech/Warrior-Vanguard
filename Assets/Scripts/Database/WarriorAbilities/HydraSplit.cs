using System.Text.RegularExpressions;
public class HydraSplit {
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

    public bool Trigger(Character target, GridManager gridManager, CharacterSpawner characterSpawner) {
        if (GetValue(target.stats)) {
            for (int i = 0; i < 3; i++) {
                GridCell randomCell = gridManager.GetRandomEmptyDeploy();
                if (!randomCell) break;

                WarriorStats stats = new HydraSerpent().GetStats();
                stats.level = target.stats.level;

                characterSpawner.Spawn(randomCell.transform.position, stats, target.alignment, target.gridPosition);
            }
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
        return $"{WarriorAbility.Keywords.Death}: Summon 3 {new HydraSerpent().GetStats().strength[stats.level]}/{new HydraSerpent().GetStats().health[stats.level]} Hydra Serpents";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}