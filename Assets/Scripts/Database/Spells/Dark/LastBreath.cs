using System.Collections.Generic;
using System.Threading.Tasks;

public class LastBreath {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 5,
            spellTarget = SpellTarget.enemy,
            spellDescription = new string[] {
            "Set an enemy's health to 1",
            "Kill an enemy"
            },
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new();
        if (cardLevel == 0) {
            target.stats.AddHealthCurrent(-(target.stats.GetHealth() - 1));
            target.UpdateWarriorUI();
        } else {
            asyncFunctions.Add(target.Die(target));
        }
        asyncFunctions.Add(floatingText.CreateFloatingText(target.transform, "Last Breath", ColorPalette.ColorEnum.purple));
        await Task.WhenAll(asyncFunctions);
    }
}