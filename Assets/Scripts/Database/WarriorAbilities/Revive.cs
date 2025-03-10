using System.Text.RegularExpressions;
public class Revive {
    bool value = false;

    public void Add() {
        value = true;
    }

    public void Remove() {
        value = false;
    }

    public bool Trigger(Character character, GridManager gridManager, CharacterSpawner characterSpawner) {
        if (value) {
            GridCell randomCell = gridManager.GetRandomEmptyDeploy();
            if (!randomCell) return true;

            character.stats.ResetStats();
            character.stats.ability.revive.Remove();

            characterSpawner.Spawn(randomCell.transform.position, character.stats, character.alignment, character.gridPosition);
            return true;
        }
        return false;
    }

    public string GetTitle() {
        if (!value) return "";
        return $"{GetAbilityName()}\n";
    }

    public string GetDescription() {
        if (!value) return "";
        return $"{WarriorAbility.Keywords.Death}: Resummon this without {GetAbilityName()}";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}