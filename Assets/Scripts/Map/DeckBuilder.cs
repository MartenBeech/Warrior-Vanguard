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
    private GameObject cardInstance;

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
        for (int i = 0; i < deck.Count; i++) {
            cardInstance = Instantiate(cardPrefab, new Vector2(800 + (100 * i), 600), Quaternion.identity, deckViewPanel.transform);
            Card cardComponent = cardInstance.GetComponentInChildren<Card>();
            cardComponent.SetStats(deck[i].stats);
            cardComponent.UpdateCardUi();
        }
    }

    public void ToggleDeckView() {
        isDeckViewOpen = !isDeckViewOpen;
        deckViewPanel.SetActive(isDeckViewOpen);
    }
}
