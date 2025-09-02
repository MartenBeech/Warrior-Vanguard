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
            race = Warrior.Race.Dark,
            cardType = CardType.Spell,
        };
        stats.genre = (Warrior.Genre)Enum.Parse(typeof(Warrior.Genre), stats.race.ToString());

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        WarriorSummoner.Alignment alignment = WarriorSummoner.Alignment.Null;
        if (parameters.target.stats.alignment == WarriorSummoner.Alignment.Enemy) {
            alignment = WarriorSummoner.Alignment.Friend;
        } else if (parameters.target.stats.alignment == WarriorSummoner.Alignment.Friend) {
            alignment = WarriorSummoner.Alignment.Enemy;
        }

        List<Task> asyncFunctions = new() {
            parameters.target.Die(parameters.target),
            parameters.target.stats.ability.raiseDead.SummonSkeleton(parameters.target, parameters.target, parameters.warriorSummoner, alignment)
        };

        if (parameters.cardLevel == 1) {
            asyncFunctions.Add(parameters.target.stats.ability.raiseDead.SummonSkeleton(parameters.target, parameters.target, parameters.warriorSummoner, alignment));
        }

        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Boned", ColorPalette.ColorEnum.Purple);
        await Task.WhenAll(asyncFunctions);
    }
}