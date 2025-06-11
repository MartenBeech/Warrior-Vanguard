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
    List<Card> cardsInHand = new();

    int handSize = 0;

    public void AddCardToHand(WarriorStats stats) {
        if (handSize >= 10) return;

        GameObject cardInstance = Instantiate(cardPrefab, new Vector2(0, 0), Quaternion.identity, handObject);

        Card card = cardInstance.GetComponent<Card>();
        card.SetHand(this);
        card.stats.SetStats(stats);
        card.SetHoverWarrior();
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
        if (selectedCard.stats.cardType == CardType.warrior) {
            asyncFunctions.Add(
                characterSpawner.Spawn(selectedGridIndex, selectedCard.stats, summonerObject.transform.position)
            );
        } else if (selectedCard.stats.cardType == CardType.spell) {
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

        if (card.stats.cardType == CardType.warrior) {
            gridManager.HighlightDeploys(card.stats.ability.construct.GetValue(card.stats), card.stats.alignment);
        } else if (card.stats.cardType == CardType.spell) {
            switch (card.stats.spellTarget) {
                case SpellTarget.none:
                    gridManager.HighlightAllCells();
                    break;
                case SpellTarget.enemy:
                    gridManager.HighlightEnemies(card.stats.alignment, true);
                    break;
                case SpellTarget.friend:
                    gridManager.HighlightFriends(card.stats.alignment, true);
                    break;
                case SpellTarget.warrior:
                    gridManager.HighlightWarriors(true);
                    break;
            }
        }

        card.GetComponent<Outline>().enabled = true;
        card.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.tealWeak);
    }

    public void DeselectCard(Card card) {
        if (!card) return;

        GridManager gridManager = FindFirstObjectByType<GridManager>();
        gridManager.ClearHighlightedCells();

        selectedCard = null;
        card.GetComponent<Outline>().enabled = false;
        card.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
    }

    public List<Card> GetCardsInHand() {
        return cardsInHand;
    }

    public void UpdateDisabledCardsUI() {
        foreach (var card in cardsInHand) {
            card.UpdateDisabledUI();
        }
    }
}