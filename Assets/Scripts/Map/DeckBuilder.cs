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
        List<WarriorStats> deckStats = DeckManager.GetDeck();

        foreach (WarriorStats stat in deckStats) {
            Card card = new();
            card.SetStats(stat);
            AddCardToDeck(card);
        }
    }

    public void AddCardToDeck(Card card) {
        deck.Add(card);
        UpdateDeckUI();
    }

    public void RemoveCardFromDeck(Card card) {
        if (!deck.Contains(card)) return;

        deck.Remove(card);
        UpdateDeckUI();
    }

    private void UpdateDeckUI() {
        if (textObject) textObject.GetComponent<TMP_Text>().text = $"{deck.Count}";

        foreach (Transform child in deckListContainer) {
            Destroy(child.gameObject);
        }

        foreach (Card card in deck) {
            GameObject cardItem = Instantiate(cardPrefab, deckListContainer);
            cardItem.transform.localScale = new Vector2(1.5f, 1.5f);
            Card cardComponent = cardItem.GetComponent<Card>();
            cardComponent.SetStats(card.stats);
            cardComponent.UpdateCardUi();
        }

        DeckManager.SetDeck(deck);
    }

    public void ToggleDeckView() {
        isDeckViewOpen = !isDeckViewOpen;
        deckViewPanel.SetActive(isDeckViewOpen);

        if (isDeckViewOpen) {
            UpdateDeckUI();
        }
    }

    public void HideDeckView() {
        isDeckViewOpen = false;
        deckViewPanel.SetActive(false);
    }
}
