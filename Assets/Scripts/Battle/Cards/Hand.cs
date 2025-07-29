using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hand : MonoBehaviour {
    public Transform handObject;
    public GameObject cardPrefab;
    public Card selectedCard;
    public GridManager gridManager;
    public FloatingText floatingText;
    public Transform warriors;
    List<Card> cardsInHand = new();

    int handSize = 0;

    public void AddCardToHand(WarriorStats stats) {
        if (handSize >= 10) return;

        GameObject cardInstance = Instantiate(cardPrefab, new Vector2(0, 0), Quaternion.identity, handObject);

        Card card = cardInstance.GetComponent<Card>();
        card.SetHand(this);
        card.stats.SetStats(stats);
        card.SetHoverCard();
        card.SetGameManager();
        card.UpdateCardUI();

        cardsInHand.Add(card);

        Button cardButton = cardInstance.GetComponent<Button>();
        cardButton.onClick.AddListener(() => { card.OnClick(); });

        handSize++;
    }

    public async Task PlayCardFromHand(CharacterSpawner characterSpawner, Vector2 selectedGridIndex) {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        Coin coin = null;
        GameObject summonerObject = null;
        if (selectedCard.stats.alignment == CharacterSpawner.Alignment.Friend) {
            coin = gameManager.friendCoin;
            summonerObject = gameManager.friendSummonerObject;
        } else if (selectedCard.stats.alignment == CharacterSpawner.Alignment.Enemy) {
            coin = gameManager.enemyCoin;
            summonerObject = gameManager.enemySummonerObject;
        }

        if (!coin.SpendCoins(selectedCard.stats.GetCost())) return;

        List<Task> asyncFunctions = new();
        if (selectedCard.stats.cardType == CardType.Warrior) {
            asyncFunctions.Add(
                characterSpawner.Spawn(selectedGridIndex, selectedCard.stats, summonerObject.transform.position)
            );
        } else if (selectedCard.stats.cardType == CardType.Spell) {
            Type type = Type.GetType(selectedCard.stats.title);
            object instance = Activator.CreateInstance(type);
            Character target = gridManager.GetCellCharacter(selectedGridIndex);
            asyncFunctions.Add((Task)type.GetMethod("Trigger")?.Invoke(instance, new object[] { gridManager, target, selectedCard.stats.level, floatingText, characterSpawner }));
        }

        cardsInHand.Remove(selectedCard);
        Destroy(selectedCard.gameObject);
        DeselectCard(selectedCard);
        handSize--;
        await Task.WhenAll(asyncFunctions);
    }

    public void SelectCard(Card card) {
        selectedCard = card;
        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
        characterSpawner.ActivateSpawn(CharacterSpawner.Alignment.Friend);

        GridManager gridManager = FindFirstObjectByType<GridManager>();

        if (card.stats.cardType == CardType.Warrior) {
            gridManager.HighlightDeploys(card.stats.ability.construct.GetValue(card.stats), card.stats.alignment);
        } else if (card.stats.cardType == CardType.Spell) {
            switch (card.stats.spellTarget) {
                case SpellTarget.None:
                    gridManager.HighlightAllCells();
                    break;
                case SpellTarget.Enemy:
                    gridManager.HighlightEnemies(card.stats.alignment, true);
                    break;
                case SpellTarget.Friend:
                    gridManager.HighlightFriends(card.stats.alignment, true);
                    break;
                case SpellTarget.Warrior:
                    gridManager.HighlightWarriors(true);
                    break;
            }
        }

        card.GetComponent<Outline>().enabled = true;
        card.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.TealWeak);
    }

    public void DeselectCard(Card card) {
        if (!card) return;

        GridManager gridManager = FindFirstObjectByType<GridManager>();
        gridManager.ClearHighlightedCells();

        selectedCard = null;
        card.GetComponent<Outline>().enabled = false;
        card.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.White);
    }

    public List<Card> GetCardsInHand() {
        return cardsInHand;
    }

    public void UpdateDisabledCardsUI() {
        foreach (var card in cardsInHand) {
            card.UpdateDisabledUI();
        }
    }

    public void ReduceCostRandomCard(int amount) {
        if (cardsInHand.Count == 0) return;
        List<int> indexesWithCost = new();

        for (int i = 0; i < cardsInHand.Count; i++) {
            if (cardsInHand[i].stats.GetCost() > 0) {
                indexesWithCost.Add(i);
            }
        }

        if (indexesWithCost.Count == 0) return;

        int randomCard = UnityEngine.Random.Range(0, indexesWithCost.Count);
        int cardIndex = indexesWithCost[randomCard];
        cardsInHand[cardIndex].stats.ReduceCost(amount);
    }

    public void ReduceCostAllCards(int amount) {
        foreach (var card in cardsInHand) {
            card.stats.ReduceCost(amount);
            card.UpdateCardUI();
        }
    }

    public void ReduceCostRace(int amount, Character.Race race) {
        foreach (var card in cardsInHand) {
            if (card.stats.race == race) {
                card.stats.ReduceCost(amount);
                card.UpdateCardUI();
            }
        }
    }

    public void IncreaseCostAllCards(int amount) {
        foreach (var card in cardsInHand) {
            card.stats.IncreaseCost(amount);
            card.UpdateCardUI();
        }
    }

    public async Task MoveNewCardToHand(WarriorStats stats, Vector2 fromPos) {
        Vector2 handPos = handObject.transform.position;

        GameObject cardInstance = Instantiate(cardPrefab, fromPos, Quaternion.identity, warriors);
        ObjectAnimation objectAnimation = cardInstance.GetComponentInChildren<ObjectAnimation>();
        Card card = cardInstance.GetComponentInChildren<Card>();

        card.SetStats(stats);
        card.UpdateCardUI();

        await objectAnimation.MoveObject(fromPos, handPos);

        Destroy(cardInstance);

        AddCardToHand(card.stats);
    }
}