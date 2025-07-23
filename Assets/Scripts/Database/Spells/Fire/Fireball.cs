using System.Collections.Generic;
using System.Threading.Tasks;

public class Fireball {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            rarity = CardRarity.Rare,
            spellTarget = SpellTarget.warrior,
            spellDescription = new string[] {
            "Deal 5 damage to a warrior and 2 damage to surrounding warriors",
            "Deal 6 damage to a warrior and 3 damage to surrounding warriors"
            },
            race = Character.Race.Fire,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new();

        int baseDamage = cardLevel == 0 ? 5 : 6;
        int surroundingDamage = cardLevel == 0 ? 2 : 3;

        await target.TakeDamage(target, baseDamage, Character.DamageType.Magical);

        List<Character> nearbyWarriors = gridManager.GetNearbyWarriors(target.gridIndex);
        foreach (Character warrior in nearbyWarriors) {
            asyncFunctions.Add(warrior.TakeDamage(warrior, surroundingDamage, Character.DamageType.Magical));
        }

        target.UpdateWarriorUI();
    }
}