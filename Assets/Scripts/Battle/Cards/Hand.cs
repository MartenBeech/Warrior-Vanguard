using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    public Transform handObject;
    public GameObject cardPrefab;
    public Card selectedCard;
    List<Card> cardsInHand = new();

    int handSize = 0;

    public void AddCardToHand(WarriorStats stats) {
        if (handSize >= 10) return;

        GameObject cardInstance = Instantiate(cardPrefab, new Vector2(0, 0), Quaternion.identity, handObject);

        Card card = cardInstance.GetComponent<Card>();
        card.SetHand(this);
        card.stats.SetStats(stats);
        card.UpdateCardUi();
        card.SetHoverWarrior();

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
        coin.SpendCoins(selectedCard.stats.cost);

        List<Task> asyncFunctions = new() {
            characterSpawner.Spawn(selectedGridIndex, selectedCard.stats, summonerObject.transform.position)
        };

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
        gridManager.HighlightDeploys(card.stats.ability.construct.Trigger(card.stats), card.stats.alignment);

        card.GetComponent<Outline>().enabled = true;
        card.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.tealWeak);
    }

    public void DeselectCard(Card card) {
        if (!card) return;

        GridManager gridManager = FindFirstObjectByType<GridManager>();
        gridManager.ClearHighlightedDeploys();

        selectedCard = null;
        card.GetComponent<Outline>().enabled = false;
        card.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
    }

    public List<Card> GetCardsInHand() {
        return cardsInHand;
    }
}