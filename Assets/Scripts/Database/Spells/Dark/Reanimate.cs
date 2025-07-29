using System.Threading.Tasks;

public class Reanimate {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            spellTarget = SpellTarget.Friend,
            spellDescription = new string[] {
            "Give a friend 'Revive' and set its health to 1",
            "Give a friend 'Revive'"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.ability.revive.Add();
        if (cardLevel == 0) {
            target.stats.AddHealthCurrent(-(target.stats.GetHealth() - 1));
        }
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Reanimate", ColorPalette.ColorEnum.Teal);
    }
}