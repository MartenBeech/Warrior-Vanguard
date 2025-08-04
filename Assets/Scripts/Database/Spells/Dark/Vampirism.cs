using System.Collections.Generic;
using System.Threading.Tasks;

public class Vampirism {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend: Lifesteal. Set its health to 1",
            "Give a friend: Lifesteal"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.ability.lifeSteal.Add();
        if (cardLevel == 0) {
            target.stats.SetHealthCurrent(1);
        }
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Vampirism", ColorPalette.ColorEnum.Yellow);
    }
}