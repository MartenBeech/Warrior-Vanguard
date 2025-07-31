using System.Collections.Generic;
using System.Threading.Tasks;

public class SkinToBones {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 6 },
            spellTarget = SpellTarget.Enemy,
            spellDescription = new string[] {
            "Kill an enemy and summon a skeleton",
            "Kill an enemy and summon 2 skeletons"
            },
            race = Character.Race.Dark,
            cardType = CardType.Spell,
        };

        return stats;
    }

    public async Task Trigger(GridManager gridManager, Character target, int cardLevel, FloatingText floatingText, CharacterSpawner characterSpawner) {
        CharacterSpawner.Alignment alignment = CharacterSpawner.Alignment.Null;
        if (target.stats.alignment == CharacterSpawner.Alignment.Enemy) {
            alignment = CharacterSpawner.Alignment.Friend;
        } else if (target.stats.alignment == CharacterSpawner.Alignment.Friend) {
            alignment = CharacterSpawner.Alignment.Enemy;
        }

        List<Task> asyncFunctions = new() {
            target.Die(target),
            target.stats.ability.raiseDead.SummonSkeleton(target, target, characterSpawner, alignment)
        };

        if (cardLevel == 1) {
            asyncFunctions.Add(target.stats.ability.raiseDead.SummonSkeleton(target, target, characterSpawner, alignment));
        }

        await floatingText.CreateFloatingText(target.transform, "Boned", ColorPalette.ColorEnum.Purple);
        await Task.WhenAll(asyncFunctions);
    }
}