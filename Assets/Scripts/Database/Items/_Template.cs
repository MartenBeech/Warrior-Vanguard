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

    public override void UseOnFriendSpawn(WarriorStats stats) {

    }

    public override async Task UseAfterFriendSpawn(WarriorStats stats, Vector2 gridIndex) {
        await Task.Delay(0);
    }

    public override void UseOnEnemySpawn(WarriorStats stats) {

    }

    public override async Task UseAfterEnemySpawn(WarriorStats stats, Vector2 gridIndex) {
        await Task.Delay(0);
    }

    public override async Task UseStartOfCombat(Summoner summoner) {
        await Task.Delay(0);
    }

    public override async Task UseStartOfTurn(Summoner summoner, Deck ownDeck, Deck enemyDeck, Hand enemyHand) {
        await Task.Delay(0);
    }

    public override async Task UseOnWarriorDeath(Summoner summoner) {
        await Task.Delay(0);
    }
}