public class WarriorAbility {
    public enum Ability {
        Bloodlust,
    }

    public string GetAbilityText(Card card) {
        string returnValue = "";

        if (card.stats.abilities.ContainsKey(Ability.Bloodlust)) {
            returnValue += $"Bloodlust {card.stats.abilities[Ability.Bloodlust]}";
        }

        return returnValue;
    }
}