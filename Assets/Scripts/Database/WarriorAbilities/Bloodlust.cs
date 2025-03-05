public class Bloodlust : WarriorAbility {
    public static void Increase(WarriorStats stats, int amount) {
        if (stats.abilities.ContainsKey(Ability.Bloodlust)) {
            stats.abilities[Ability.Bloodlust] += amount;
        } else {
            stats.abilities[Ability.Bloodlust] = amount;
        }
    }

    public static void Trigger(Character character) {
        character.stats.attack += character.stats.abilities[Ability.Bloodlust];
        character.UpdateWarriorUI();
    }
}