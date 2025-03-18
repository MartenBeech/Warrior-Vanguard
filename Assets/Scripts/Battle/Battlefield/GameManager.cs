using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {
    List<Character> friends = new();
    List<Character> enemies = new();
    public GridManager gridManager;
    public GameObject friendSummonerObject;
    public GameObject enemySummonerObject;
    public Coin coin;

    void Awake() {
        Summoner friendSummoner = friendSummonerObject.GetComponent<Summoner>();
        friendSummoner.SetStats(Angel.GetSummoner());
        Summoner enemySummoner = enemySummonerObject.GetComponent<Summoner>();
        enemySummoner.SetStats(Devil.GetSummoner());
    }

    public async void EndTurn() {
        List<Character> sortedFriends = friends.OrderByDescending(c => c.gridIndex.x).ToList();
        foreach (Character friend in sortedFriends) {
            friend.SetRemainingActions(friend.stats.numberOfAttacks, friend.stats.speed);
            int maxActions = friend.stats.numberOfAttacks + friend.stats.speed;
            for (int i = 0; i < maxActions; i++) {
                await friend.MoveWarrior(Character.Direction.Right);
            }

            if (friend.remainingAttacks > 0) {
                await friend.StandAndAttack(Character.Direction.Right);
            }
        }
        coin.GainCoins();
        coin.RefreshCoins();
    }

    public async void EndEnemyTurn() {
        enemies = enemies.OrderBy(c => c.gridIndex.x).ToList();
        foreach (Character enemy in enemies) {
            enemy.SetRemainingActions(enemy.stats.numberOfAttacks, enemy.stats.speed);
            int maxActions = enemy.stats.numberOfAttacks + enemy.stats.speed;
            for (int i = 0; i < maxActions; i++) {
                await enemy.MoveWarrior(Character.Direction.Left);
            }

            if (enemy.remainingAttacks > 0) {
                await enemy.StandAndAttack(Character.Direction.Left);
            }
        }
    }

    public void RegisterCharacter(Character character, CharacterSpawner.Alignment alignment) {
        if (alignment == CharacterSpawner.Alignment.Enemy) {
            if (!enemies.Contains(character)) {
                enemies.Add(character);
            }
        } else if (alignment == CharacterSpawner.Alignment.Friend) {
            if (!friends.Contains(character)) {
                friends.Add(character);
            }
        }
        gridManager.RegisterCharacter(character);
    }

    public void RemoveCharacter(Character character) {
        if (friends.Contains(character))
            friends.Remove(character);
        else if (enemies.Contains(character))
            enemies.Remove(character);
    }
}
