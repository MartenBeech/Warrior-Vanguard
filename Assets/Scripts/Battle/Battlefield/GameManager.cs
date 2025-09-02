using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    List<Warrior> friends = new();
    List<Warrior> enemies = new();
    public GridManager gridManager;
    public GameObject friendSummonerObject;
    public GameObject enemySummonerObject;
    public Coin friendCoin;
    public Coin enemyCoin;
    public Deck friendDeck;
    public Deck enemyDeck;
    public Hand friendHand;
    public Hand enemyHand;
    public Item enemyItem;
    public Summoner friendSummoner;
    Summoner enemySummoner;
    WarriorSummoner friendWarriorSummoner;
    WarriorSummoner enemyWarriorSummoner;
    public HeroPower heroPower;
    public List<GameObject> interactableButtons;
    public static WarriorSummoner.Alignment turn;
    public static string enemySummonerName = "";
    public static bool isLoading = false;
    private FloatingText floatingText;

    async void Awake() {
        SetLoading(true);
        friendSummoner.SetStats(new SummonerStats(FriendlySummoner.summonerData.title, FriendlySummoner.GetHealth(), FriendlySummoner.GetMaxHealth()));
        heroPower.SetHeroPower(friendSummoner.stats.heroPowerTitle, friendSummoner.stats.heroPowerDescription, friendSummoner.stats.heroPowerCost, friendSummoner.stats.heroPowerImage, friendSummoner.stats.heroPowerEffect);
        friendDeck.GetDeck();
        floatingText = FindFirstObjectByType<FloatingText>();

        enemySummoner = enemySummonerObject.GetComponent<Summoner>();
        Type type = Type.GetType(enemySummonerName);
        object instance = Activator.CreateInstance(type);
        enemyDeck.deckList = (List<WarriorStats>)type.GetMethod("GetDeck")?.Invoke(instance, null);
        enemyDeck.ShuffleDeck(enemyDeck.deckList);
        enemySummoner.SetStats((SummonerStats)type.GetMethod("GetSummoner")?.Invoke(instance, null));
        enemyItem.SetItem(ItemManager.enemyItem);

        List<Task> asyncFunctions = new();
        for (int i = 0; i < 3; i++) {
            asyncFunctions.Add(friendDeck.DrawCard(false));
            asyncFunctions.Add(enemyDeck.DrawCard(false));
        }
        await Task.WhenAll(asyncFunctions);

        foreach (Item item in ItemManager.items) {
            item.triggeredThisCombat = false;
            await item.UseStartOfCombat(new(summoner: friendSummoner));
            await item.UseStartOfEnemyCombat(new(summoner: enemySummoner));
        }

        if (enemySummoner.stats.ability.summonMobyRichard.GetValue(enemySummoner.stats)) {
            enemyWarriorSummoner = FindFirstObjectByType<WarriorSummoner>();
            enemyWarriorSummoner.summoningAlignment = WarriorSummoner.Alignment.Enemy;
            await enemySummoner.stats.ability.summonMobyRichard.TriggerStartOfCombat(enemySummoner, enemyWarriorSummoner);
        }

        if (friendSummoner.stats.ability.summonFlotSam.GetValue(friendSummoner.stats)) {
            friendWarriorSummoner = FindFirstObjectByType<WarriorSummoner>();
            friendWarriorSummoner.summoningAlignment = WarriorSummoner.Alignment.Friend;
            await friendSummoner.stats.ability.summonFlotSam.TriggerStartOfCombat(friendSummoner, friendWarriorSummoner);
        }

        await ItemManager.enemyItem.UseStartOfCombat(new(summoner: enemySummoner));

        await StartPlayerTurn();
    }

    public async Task StartPlayerTurn() {
        turn = WarriorSummoner.Alignment.Friend;
        friendCoin.GainCoins();
        friendCoin.RefreshCoins();
        heroPower.RefreshHeroPower();

        await friendDeck.DrawCard();

        foreach (Item item in ItemManager.items) {
            await item.UseStartOfTurn(new(
                summoner: friendSummonerObject.GetComponent<Summoner>(),
                ownDeck: friendDeck,
                enemyDeck: enemyDeck,
                enemyHand: enemyHand,
                gridManager: gridManager,
                floatingText: floatingText));
        }

        foreach (Warrior friend in friends) {
            friend.StartTurn();
        }

        SetLoading(false);
    }

    public async void EndPlayerTurn() {
        SetLoading(true);
        List<Warrior> sortedFriends = friends.OrderByDescending(c => c.gridIndex.x).ToList();
        foreach (Warrior friend in sortedFriends) {
            await friend.MoveWarrior(Warrior.Direction.Right);
            await friend.StandAndAttack(Warrior.Direction.Right);
            await friend.EndTurn();
        }

        await friendSummoner.EndTurn(friendWarriorSummoner);
        await StartEnemyTurn();
    }

    public async Task StartEnemyTurn() {
        turn = WarriorSummoner.Alignment.Enemy;
        enemyCoin.GainCoins();
        enemyCoin.RefreshCoins();
        await enemyDeck.DrawCard(false);

        await ItemManager.enemyItem.UseStartOfTurn(new(
                summoner: friendSummonerObject.GetComponent<Summoner>(),
                ownDeck: enemyDeck,
                enemyDeck: friendDeck,
                enemyHand: enemyHand,
                gridManager: gridManager,
                floatingText: floatingText));

        foreach (Warrior enemy in enemies) {
            enemy.StartTurn();
        }

        await TakeEnemyTurn();
    }

    public async void EndEnemyTurn() {
        List<Warrior> sortedEnemies = enemies.OrderBy(c => c.gridIndex.x).ToList();
        foreach (Warrior enemy in sortedEnemies) {
            await enemy.MoveWarrior(Warrior.Direction.Left);
            await enemy.StandAndAttack(Warrior.Direction.Left);
            await enemy.EndTurn();
        }

        enemyWarriorSummoner.summoningAlignment = WarriorSummoner.Alignment.Enemy;
        await enemySummoner.EndTurn(enemyWarriorSummoner);
        await StartPlayerTurn();
    }

    public void RegisterWarrior(Warrior warrior, WarriorSummoner.Alignment alignment) {
        if (alignment == WarriorSummoner.Alignment.Enemy) {
            if (!enemies.Contains(warrior)) {
                enemies.Add(warrior);
            }
        } else if (alignment == WarriorSummoner.Alignment.Friend) {
            if (!friends.Contains(warrior)) {
                friends.Add(warrior);
            }
        }
        gridManager.RegisterWarrior(warrior);
    }

    public void RemoveWarrior(Warrior warrior) {
        if (friends.Contains(warrior))
            friends.Remove(warrior);
        else if (enemies.Contains(warrior))
            enemies.Remove(warrior);
    }

    public void WinFight() {
        LevelManager.CompleteLevel();
    }

    public void LoseFight() {
        LevelManager.LoseLevel();
    }

    async Task TakeEnemyTurn() {
        await Task.Delay(1000 / Settings.gameSpeed);
        enemyWarriorSummoner = FindFirstObjectByType<WarriorSummoner>();
        List<Card> cardsInHand = new(enemyHand.GetCardsInHand());
        cardsInHand.Sort((a, b) => b.stats.GetCost() - a.stats.GetCost());
        foreach (Card card in cardsInHand) {
            if (enemyCoin.CanAfford(card.stats.GetCost())) {
                enemyHand.SelectCard(card);
                GridCell randomCell = gridManager.GetRandomHighlighedCell();
                if (!randomCell) {
                    enemyHand.DeselectCard(card);
                    break;
                }
                await enemyHand.PlayCardFromHand(enemyWarriorSummoner, randomCell.gridIndex);
                await Task.Delay(1000 / Settings.gameSpeed);
            }
        }
        EndEnemyTurn();
    }

    public void SetLoading(bool value) {
        isLoading = value;

        foreach (var clickableButton in interactableButtons) {
            clickableButton.GetComponent<Button>().interactable = !isLoading;
        }
        foreach (var card in friendHand.GetComponentsInChildren<Button>()) {
            card.interactable = !isLoading;
        }
    }
}

