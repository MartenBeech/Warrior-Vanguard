using System.Collections.Generic;
using System.Threading.Tasks;

public class CLASSNAMESPELL {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 0, 0 },
            rarity = CardRarity.None,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "UNUPGRADED_DESCRIPTION",
            "UPGRADED_DESCRIPTION"
            },
            race = Character.Race.None,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new();

        target.UpdateWarriorUI();
        asyncFunctions.Add(floatingText.CreateFloatingText(target.transform, "TEXT", ColorPalette.ColorEnum.Purple));
        await Task.WhenAll(asyncFunctions);
    }
}