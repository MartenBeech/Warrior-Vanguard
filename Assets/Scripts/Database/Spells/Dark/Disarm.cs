using System.Threading.Tasks;

public class Disarm {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 2 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Set an enemy's strength to 1",
            "Set an enemy's strength to 1"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        target.stats.AddStrength(-(target.stats.GetStrength() - 1));
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Disarmed", ColorPalette.ColorEnum.Purple);
    }
}