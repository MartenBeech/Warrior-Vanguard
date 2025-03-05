using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckBuilder : MonoBehaviour {
    public List<Card> deck = new();
    public CardDatabase cardDatabase;
    public GameObject textObject;
    public GameObject deckViewPanel;
    private bool isDeckViewOpen = false;
    public GameObject cardPrefab;
    public Transform deckListContainer;

    private void Start() {
        deckViewPanel.SetActive(false);
    }

    public void AddCardToDeck() {
        Card card = new();
        card.SetStats(CardDatabase.Instance.allCards[1]);
        deck.Add(card);
        UpdateDeckUI();
    }

    public void RemoveCardFromDeck() {
        if (deck.Count == 0) return;
        deck.Remove(deck[0]);
        UpdateDeckUI();
    }

    private void UpdateDeckUI() {
        if (textObject) textObject.GetComponent<TMP_Text>().text = $"{deck.Count}";

        foreach (Transform child in deckListContainer) {
            Destroy(child.gameObject);
        }

        foreach (Card card in deck) {
            GameObject cardItem = Instantiate(cardPrefab, deckListContainer);
            Card cardComponent = cardItem.GetComponent<Card>();
            cardComponent.SetStats(card.stats);
            cardComponent.UpdateCardUi();
        }
    }

    public void ToggleDeckView() {
        isDeckViewOpen = !isDeckViewOpen;
        deckViewPanel.SetActive(isDeckViewOpen);

        if (isDeckViewOpen) {
            UpdateDeckUI();
        }
    }
}
