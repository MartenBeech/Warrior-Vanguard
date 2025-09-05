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
            race = Race.Dark,
            genre = Genre.Undead,
            cardType = CardType.Spell,
        };
        

        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        Alignment alignment = Alignment.Null;
        if (parameters.target.stats.alignment == Alignment.Enemy) {
            alignment = Alignment.Friend;
        } else if (parameters.target.stats.alignment == Alignment.Friend) {
            alignment = Alignment.Enemy;
        }

        List<Task> asyncFunctions = new() {
            parameters.target.Die(parameters.target),
            parameters.target.stats.ability.raiseDead.SummonSkeleton(parameters.target, parameters.target, parameters.warriorSummoner, alignment)
        };

        if (parameters.cardLevel == 1) {
            asyncFunctions.Add(parameters.target.stats.ability.raiseDead.SummonSkeleton(parameters.target, parameters.target, parameters.warriorSummoner, alignment));
        }

        await parameters.floatingText.CreateFloatingText(parameters.target.transform, "Boned", ColorEnum.Purple);
        await Task.WhenAll(asyncFunctions);
    }
}