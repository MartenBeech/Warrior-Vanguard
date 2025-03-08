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
            WarriorStats stats = Mario.GetCard();
            stats.ability.revive.Remove();

            characterSpawner.Spawn(randomCell.transform.position, stats, character.alignment, character.gridPosition);
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
        return $"{GetTitle()}On death: resummon this minion without {GetAbilityName()}";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}