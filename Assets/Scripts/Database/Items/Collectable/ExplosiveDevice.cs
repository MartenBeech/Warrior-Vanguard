using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExplosiveDevice : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When any warriors dies, deal 2 damage to all nearby warriors";
        rarity = ItemManager.Rarity.Normal;
        return this;
    }

    public override async Task UseOnWarriorDeath(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Warrior> nearbyWarriors = gridManager.GetNearbyWarriors(parameters.gridIndex);

        List<Task> asyncFunctions = new();
        foreach (var nearbyWarrior in nearbyWarriors) {
            asyncFunctions.Add(nearbyWarrior.TakeDamage(gridManager.GetCellWarrior(parameters.gridIndex), 2, Warrior.DamageType.Magical));
        }
        await Task.WhenAll(asyncFunctions);
    }
}