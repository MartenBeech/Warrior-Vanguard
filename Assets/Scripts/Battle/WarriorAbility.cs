public class WarriorAbility {
    public enum Ability {
        Bloodlust,  //Bloodlust X: After attacking, gain X strength
        Revive,     //Revive: First time this dies, summon a copy
    }

    public string GetAbilityText(Card card) {
        string returnValue = "";

        if (card.stats.abilities.ContainsKey(Ability.Bloodlust)) {
            returnValue += $"Bloodlust {card.stats.abilities[Ability.Bloodlust]}\n";
        }
        if (card.stats.abilities.ContainsKey(Ability.Revive)) {
            returnValue += $"Revive\n";
        }

        return returnValue;
    }
}