public class Bloodlust : WarriorAbility {
    public static void Add(WarriorStats stats, int amount) {
        if (stats.abilities.ContainsKey(Ability.Bloodlust)) {
            stats.abilities[Ability.Bloodlust] += amount;
        } else {
            stats.abilities[Ability.Bloodlust] = amount;
        }
    }

    public static bool Trigger(Character character) {
        if (character.stats.abilities.ContainsKey(Ability.Bloodlust)) {
            character.stats.attack += character.stats.abilities[Ability.Bloodlust];
            character.UpdateWarriorUI();
            return true;
        }
        return false;
    }
}