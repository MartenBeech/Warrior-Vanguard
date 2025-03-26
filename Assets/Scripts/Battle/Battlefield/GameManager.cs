using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour {
    List<Character> friends = new();
    List<Character> enemies = new();
    public GridManager gridManager;
    public GameObject friendSummonerObject;
    public GameObject enemySummonerObject;
    public Coin friendCoin;
    public Coin enemyCoin;
    public Deck friendDeck;
    public Deck enemyDeck;
    public Hand enemyHand;
    public static CharacterSpawner.Alignment turn;

    async void Awake() {
        Summoner friendSummoner = friendSummonerObject.GetComponent<Summoner>();
        friendSummoner.SetStats(new SummonerStats(Angel.title, Angel.description, Angel.currentHealth, Angel.maxHealth));
        Summoner enemySummoner = enemySummonerObject.GetComponent<Summoner>();
        enemySummoner.SetStats(new Devil().GetSummoner());
        friendDeck.GetDeck();
        enemyDeck.GetDeck();
        List<Task> asyncFunctions = new();
        for (int i = 0; i < 3; i++) {
            asyncFunctions.Add(friendDeck.DrawCard(CharacterSpawner.Alignment.Friend, false));
            asyncFunctions.Add(enemyDeck.DrawCard(CharacterSpawner.Alignment.Enemy, false));
        }
        await Task.WhenAll(asyncFunctions);
        await StartPlayerTurn();
    }

    public async Task StartPlayerTurn() {
        turn = CharacterSpawner.Alignment.Friend;
        friendCoin.GainCoins();
        friendCoin.RefreshCoins();
        await friendDeck.DrawCard(CharacterSpawner.Alignment.Friend);
    }

    public async void EndPlayerTurn() {
        List<Character> sortedFriends = friends.OrderByDescending(c => c.gridIndex.x).ToList();
        foreach (Character friend in sortedFriends) {
            await friend.MoveWarrior(Character.Direction.Right);
            await friend.StandAndAttack(Character.Direction.Right);
            await friend.EndTurn();
        }
        await StartEnemyTurn();
    }

    public async Task StartEnemyTurn() {
        turn = CharacterSpawner.Alignment.Enemy;
        enemyCoin.GainCoins();
        enemyCoin.RefreshCoins();
        await enemyDeck.DrawCard(CharacterSpawner.Alignment.Enemy, false);
        await TakeEnemyTurn();
    }

    public async void EndEnemyTurn() {
        List<Character> sortedEnemies = enemies.OrderBy(c => c.gridIndex.x).ToList();
        foreach (Character enemy in sortedEnemies) {
            await enemy.MoveWarrior(Character.Direction.Left);
            await enemy.StandAndAttack(Character.Direction.Left);
            await enemy.EndTurn();
        }
        await StartPlayerTurn();
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

    public void WinFight() {
        LevelManager.CompleteLevel();
    }

    public void LoseFight() {
        LevelManager.LoseLevel();
    }

    async Task TakeEnemyTurn() {
        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
        List<Card> cardsInHand = new(enemyHand.GetCardsInHand());
        cardsInHand.Sort((a, b) => b.stats.cost - a.stats.cost);
        foreach (Card card in cardsInHand) {
            if (enemyCoin.SpendCoins(card.stats.cost)) {
                enemyHand.SelectCard(card);
                GridCell randomCell = gridManager.GetRandomEmptyDeploy(card.stats.ability.construct.Trigger(card.stats), card.stats.alignment);
                await enemyHand.PlayCardFromHand(characterSpawner, randomCell.gridIndex);
            }
        }
        EndEnemyTurn();
    }
}
