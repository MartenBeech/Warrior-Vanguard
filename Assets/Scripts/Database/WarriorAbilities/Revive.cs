using System.Text.RegularExpressions;
public class Revive {
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
            GridCell randomCell = gridManager.GetRandomEmptyDeploy();
            if (!randomCell) return true;

            target.stats.ResetStats();
            target.stats.ability.revive.Add(false, false);

            characterSpawner.Spawn(randomCell.transform.position, target.stats, target.alignment, target.gridPosition);
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
        return $"{WarriorAbility.Keywords.Death}: Resummon this without {GetAbilityName()}";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}