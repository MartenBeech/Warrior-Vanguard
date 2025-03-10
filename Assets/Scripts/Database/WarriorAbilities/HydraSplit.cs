using System.Text.RegularExpressions;
public class HydraSplit {
    bool value = false;

    public void Add() {
        value = true;
    }

    public void Remove() {
        value = false;
    }

    public bool Trigger(Character character, GridManager gridManager, CharacterSpawner characterSpawner) {
        if (value) {
            for (int i = 0; i < 3; i++) {
                GridCell randomCell = gridManager.GetRandomEmptyDeploy();
                if (!randomCell) break;

                WarriorStats stats = HydraSerpent.GetStats();

                characterSpawner.Spawn(randomCell.transform.position, stats, character.alignment, character.gridPosition);
            }
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
        return $"{GetTitle()}On death: Summon 3 2/2 Hydra Serpents";
    }

    string GetAbilityName() {
        string className = GetType().Name;
        string abilityName = Regex.Replace(className, "(?<!^)([A-Z])", " $1");
        return abilityName;
    }
}