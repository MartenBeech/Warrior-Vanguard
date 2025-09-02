using System.Threading.Tasks;
using UnityEngine;

public class CLASSNAME : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "DESCRIPTION";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override void UseImmediately(ItemTriggerParams parameters) {

    }

    public override void UseOnWarriorSummon(ItemTriggerParams parameters) {

    }

    public override void UseOnFriendSummon(ItemTriggerParams parameters) {

    }

    public override void UseOnEnemySummon(ItemTriggerParams parameters) {

    }
    public override async Task UseAfterFriendSummon(ItemTriggerParams parameters) {
        await Task.Delay(0);
    }


    public override async Task UseAfterEnemySummon(ItemTriggerParams parameters) {
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