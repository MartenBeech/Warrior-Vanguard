using System;
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
    public static string enemySummonerName = "Devil";

    async void Awake() {
        Summoner friendSummoner = friendSummonerObject.GetComponent<Summoner>();
        friendSummoner.SetStats(new SummonerStats(Angel.title, Angel.description, Angel.currentHealth, Angel.maxHealth));
        friendDeck.GetDeck();

        Summoner enemySummoner = enemySummonerObject.GetComponent<Summoner>();
        Type type = Type.GetType(enemySummonerName);
        object instance = Activator.CreateInstance(type);
        enemySummoner.SetStats((SummonerStats)type.GetMethod("GetSummoner")?.Invoke(instance, null));
        enemyDeck.deck = (List<WarriorStats>)type.GetMethod("GetDeck")?.Invoke(instance, null);

        List<Task> asyncFunctions = new();
        for (int i = 0; i < 3; i++) {
            asyncFunctions.Add(friendDeck.DrawCard(false));
            asyncFunctions.Add(enemyDeck.DrawCard(false));
        }
        await Task.WhenAll(asyncFunctions);

        foreach (Item item in ItemManager.LoadItems()) {
            await item.UseStartOfCombat(friendSummoner);
        }

        await StartPlayerTurn();
    }

    public async Task StartPlayerTurn() {
        turn = CharacterSpawner.Alignment.Friend;
        friendCoin.GainCoins();
        friendCoin.RefreshCoins();
        await friendDeck.DrawCard();

        foreach (Item item in ItemManager.LoadItems()) {
            await item.UseStartOfTurn(friendSummonerObject.GetComponent<Summoner>());
        }
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
        await enemyDeck.DrawCard(false);
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
        TileCompleter.MarkTileAsCompleted();
        LevelManager.CompleteLevel();
    }

    public void LoseFight() {
        LevelManager.LoseLevel();
    }

    async Task TakeEnemyTurn() {
        await Task.Delay(1000 / Settings.gameSpeed);
        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
        List<Card> cardsInHand = new(enemyHand.GetCardsInHand());
        cardsInHand.Sort((a, b) => b.stats.cost - a.stats.cost);
        foreach (Card card in cardsInHand) {
            if (enemyCoin.CanAfford(card.stats.cost)) {
                enemyHand.SelectCard(card);
                GridCell randomCell = gridManager.GetRandomHighlighedCell();
                if (!randomCell) {
                    enemyHand.DeselectCard(card);
                    break;
                }
                await enemyHand.PlayCardFromHand(characterSpawner, randomCell.gridIndex);
                await Task.Delay(1000 / Settings.gameSpeed);
            }
        }
        EndEnemyTurn();
    }
}

