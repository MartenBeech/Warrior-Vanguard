using System.Threading.Tasks;
using UnityEngine;

public class CLASSNAME : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "DESCRIPTION";
        return this;
    }

    public override void UseImmediately() {

    }

    public override void UseOnWarriorSpawn(ItemTriggerParams parameters) {

    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {

    }

    public override void UseOnEnemySpawn(ItemTriggerParams parameters) {

    }
    public override async Task UseAfterFriendSpawn(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }


    public override async Task UseAfterEnemySpawn(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }

    public override async Task UseStartOfCombat(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }

    public override async Task UseStartOfTurn(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }

    public override async Task UseOnWarriorDeath(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }

    public override async Task UseOnFriendDeath(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }

    public override async Task UseOnEnemyDeath(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }
}