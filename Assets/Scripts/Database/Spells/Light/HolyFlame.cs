using System.Collections.Generic;
using System.Threading.Tasks;

public class HolyFlame {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Warrior,
            spellDescription = new string[] {
            "Deal 3 damage to a warrior",
            "Deal 4 damage to a warrior or 6 against undead"
            },
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 3 : 4;

        if (target.stats.genre == Character.Genre.Undead) {
            int undeadValue = cardLevel == 0 ? 3 : 6;
            value = undeadValue;
        }

        await target.TakeDamage(target, value, Character.DamageType.Magical);
        target.UpdateWarriorUI();
    }
}