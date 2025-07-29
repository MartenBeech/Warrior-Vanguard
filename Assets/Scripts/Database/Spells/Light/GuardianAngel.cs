using System.Collections.Generic;
using System.Threading.Tasks;

public class GuardianAngel {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 3 },
            rarity = CardRarity.Common,
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend: Revive",
            "Give a friend: Revive"
            },
            race = Character.Race.Light,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.ability.revive.Add();
        await floatingText.CreateFloatingText(target.transform, "Guardian Angel", ColorPalette.ColorEnum.Yellow);
    }
}