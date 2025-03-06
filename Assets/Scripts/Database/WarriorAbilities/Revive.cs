public class Revive : WarriorAbility {
    public static void Add(WarriorStats stats) {
        stats.abilities[Ability.Revive] = 1;
    }

    public static void Remove(WarriorStats stats) {
        if (stats.abilities.ContainsKey(Ability.Revive)) {
            stats.abilities.Remove(Ability.Revive);
        }
    }

    public static bool Trigger(Character character) {
        if (character.stats.abilities.ContainsKey(Ability.Revive)) {
            character.stats.health = character.stats.healthMax;
            Remove(character.stats);
            character.UpdateWarriorUI();
            return true;
        }
        return false;
    }
}