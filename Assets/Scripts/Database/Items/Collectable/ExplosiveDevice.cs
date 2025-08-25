using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ExplosiveDevice : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When any warriors dies, deal 2 damage to all nearby warriors";
        return this;
    }

    public override async Task UseOnWarriorDeath(Summoner summoner, Vector2 gridIndex) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Character> nearbyWarriors = gridManager.GetNearbyWarriors(gridIndex);

        List<Task> asyncFunctions = new();
        foreach (var nearbyWarrior in nearbyWarriors) {
            asyncFunctions.Add(nearbyWarrior.TakeDamage(gridManager.GetCellCharacter(gridIndex), 2, Character.DamageType.Magical));
        }
        await Task.WhenAll(asyncFunctions);
    }
}