using System.Threading.Tasks;

public class Disarm {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            spellTarget = SpellTarget.enemy,
            spellDescription = new string[] {
            "Set an enemy's strength to 1",
            "Set an enemy's strength to 0"
            },
            race = Character.Race.Dark,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 1 : 0;
        target.stats.AddStrength(-(target.stats.GetStrength() - value));
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Disarmed", ColorPalette.ColorEnum.purple);
    }
}