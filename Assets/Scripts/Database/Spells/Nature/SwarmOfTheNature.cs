using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SwarmOfTheNature {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 10, 10 },
            rarity = CardRarity.Legendary,
            spellTarget = SpellTarget.None,
            spellDescription = new string[] {
            "Fill your deployment area with random 1-cost warriors",
            "Fill your deployment area with random 2-cost warriors"
            },
            race = Race.Nature,
            genre = Genre.Elves,
            cardType = CardType.Spell,
        };


        return stats;
    }

    public async Task Trigger(SpellTriggerParams parameters) {
        List<Task> asyncFunctions = new();

        int cost = parameters.cardLevel == 0 ? 1 : 2;
        List<GridCell> emptyDeploymentCells = parameters.gridManager.GetEmptyDeploys(false, GameManager.turn);
        
        foreach (GridCell cell in emptyDeploymentCells) {
            WarriorStats warriorStats = CardDatabase.GetRandomWarriorWithSpecificCost(cost, GameManager.turn);
            warriorStats.SetStats(warriorStats);
            await parameters.warriorSummoner.Summon(
                cell.gridIndex,
                warriorStats,
                cell.transform.position);
        }
        await Task.WhenAll(asyncFunctions);
    }
}