using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour {
    public Transform handObject;
    public GameObject cardPrefab;
    public Card selectedCard;

    int handSize = 0;

    public void AddCardToHand(WarriorStats stats) {
        if (handSize >= 10) return;

        Vector2 pos = new(0, 0);

        GameObject cardInstance = Instantiate(cardPrefab, pos, Quaternion.identity, handObject);

        Card cardHand = cardInstance.GetComponent<Card>();
        cardHand.stats.SetStats(stats);
        cardHand.UpdateCardUi();
        cardHand.SetHoverWarrior();

        Button cardButton = cardInstance.GetComponent<Button>();
        cardButton.onClick.AddListener(() => { cardHand.OnClick(); });

        handSize++;
    }

    public void PlayCardFromHand(CharacterSpawner characterSpawner, Vector2 selectedGridIndex) {
        Coin coin = FindFirstObjectByType<Coin>();
        coin.SpendCoins(selectedCard.stats.cost);

        characterSpawner.Spawn(selectedGridIndex, selectedCard.stats, CharacterSpawner.Alignment.Friend, selectedCard.GetComponent<RectTransform>().position);

        Destroy(selectedCard.gameObject);
        DeselectCard(selectedCard);
        handSize--;
    }

    public void SelectCard(Card card) {
        selectedCard = card;
        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
        characterSpawner.ActivateSpawn(CharacterSpawner.Alignment.Friend);

        GridManager gridManager = FindFirstObjectByType<GridManager>();
        gridManager.HighlightDeploys();

        card.GetComponent<Outline>().enabled = true;

    }

    public void DeselectCard(Card card) {
        if (!card) return;

        GridManager gridManager = FindFirstObjectByType<GridManager>();
        gridManager.ClearHighlightedDeploys();

        selectedCard = null;
        card.GetComponent<Outline>().enabled = false;
    }
}