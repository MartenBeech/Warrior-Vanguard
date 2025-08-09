using System;
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
        stats.genre = (Character.Genre)Enum.Parse(typeof(Character.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        CharacterSpawner.Alignment alignment = CharacterSpawner.Alignment.Null;
        if (parameters.target.stats.alignment == CharacterSpawner.Alignment.Enemy) {
            alignment = CharacterSpawner.Alignment.Friend;
        } else if (parameters.target.stats.alignment == CharacterSpawner.Alignment.Friend) {
            alignment = CharacterSpawner.Alignment.Enemy;
        }

        List<Task> asyncFunctions = new() {
            parameters.target.Die(parameters.target),
            parameters.target.stats.ability.raiseDead.SummonSkeleton(parameters.target, parameters.target, parameters.characterSpawner, alignment)
        };

        if (parameters.cardLevel == 1) {
            asyncFunctions.Add(parameters.target.stats.ability.raiseDead.SummonSkeleton(parameters.target, parameters.target, parameters.characterSpawner, alignment));
        }

        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Boned", ColorPalette.ColorEnum.Purple);
        await Task.WhenAll(asyncFunctions);
    }
}