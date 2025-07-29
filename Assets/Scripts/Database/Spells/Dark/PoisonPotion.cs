using System.Threading.Tasks;

public class PoisonPotion {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Apply 2 poison to an enemy",
            "Apply 3 poison to an enemy"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        int value = cardLevel == 0 ? 2 : 3;
        target.stats.ability.poisoned.Add(value);
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, $"{value} poison", ColorPalette.ColorEnum.Purple);
    }
}