using System.Collections.Generic;
using System.Threading.Tasks;

public class SkinToBones {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 6,
            spellTarget = SpellTarget.enemy,
            spellDescription = new string[] {
            "Kill an enemy and summon a skeleton for its owner",
            "Kill an enemy and summon a skeleton for you"
            },
            race = Character.Race.Dark,
            cardType = CardType.spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        List<Task> asyncFunctions = new() {
            target.Die(target)
        };
        if (cardLevel == 0) {
            asyncFunctions.Add(target.stats.ability.raiseDead.SummonSkeleton(target, target, characterSpawner));
        } else {
            asyncFunctions.Add(target.stats.ability.raiseDead.SummonSkeleton(target, target, characterSpawner, target.alignment == CharacterSpawner.Alignment.Enemy ? CharacterSpawner.Alignment.Friend : CharacterSpawner.Alignment.Enemy));
        }
        target.UpdateWarriorUI();
        await floatingText.CreateFloatingText(target.transform, "Boned", ColorPalette.ColorEnum.purple);
        await Task.WhenAll(asyncFunctions);
    }
}