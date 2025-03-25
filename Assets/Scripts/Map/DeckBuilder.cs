using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeckBuilder : MonoBehaviour {
    public GameObject textObject;
    public GameObject deckViewPanel;
    public GameObject hideDeckViewButton;
    private bool isDeckViewOpen = false;
    public GameObject cardPrefab;
    public Transform deckListContainer;

    private void Start() {
        deckViewPanel.SetActive(false);
        hideDeckViewButton.SetActive(false);
        UpdateDeckUI();
    }

    public void AddCardToDeck(Card card) {
        DeckManager.AddCard(card);
        UpdateDeckUI();
    }

    public void RemoveCardFromDeck(int index) {
        DeckManager.RemoveCard(index);
        UpdateDeckUI();
    }

    private void UpdateDeckUI() {
        if (textObject) textObject.GetComponent<TMP_Text>().text = $"{DeckManager.GetDeck().Count}";

        foreach (Transform child in deckListContainer) {
            Destroy(child.gameObject);
        }

        foreach (WarriorStats stats in DeckManager.GetDeck()) {
            GameObject cardItem = Instantiate(cardPrefab, deckListContainer);
            cardItem.transform.localScale = new Vector2(1.5f, 1.5f);
            Card cardComponent = cardItem.GetComponent<Card>();
            cardComponent.SetStats(stats);
            cardComponent.UpdateCardUi();
        }
    }

    public void ToggleDeckView() {
        isDeckViewOpen = !isDeckViewOpen;
        deckViewPanel.SetActive(isDeckViewOpen);
        hideDeckViewButton.SetActive(isDeckViewOpen);

        if (isDeckViewOpen) {
            UpdateDeckUI();
        }
    }

    public void HideDeckView() {
        isDeckViewOpen = false;
        deckViewPanel.SetActive(false);
        hideDeckViewButton.SetActive(false);
    }
}
