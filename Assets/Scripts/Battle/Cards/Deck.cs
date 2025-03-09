using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Deck : MonoBehaviour {
    public Hand hand;
    public GameObject deckObject;
    public GameObject textObject;
    public GameObject handObject;
    public GameObject cardPrefab;
    public GameObject hoverWarriorObject;
    public List<WarriorStats> deck = new();

    private void Start() {
        //If accessing this page from the Map, convert the Deck.
        deck = DeckManager.GetDeck();
        if (deck.Count == 0) {
            FillDeckWithRandomCards();
        }

        UpdateDeckUi();
    }

    void FillDeckWithRandomCards() {
        if (CardDatabase.Instance == null) {
            Debug.LogError("CardDatabase Instance not found");
            return;
        }

        for (int i = 0; i < CardDatabase.Instance.allCards.Count; i++) {
            deck.Add(CardDatabase.Instance.allCards[i]);
        }
    }
    public async void DrawCard() {
        if (deck.Count == 0) return;

        WarriorStats drawnCard = deck[0];
        deck.RemoveAt(0);

        UpdateDeckUi();

        Vector2 deckPos = deckObject.transform.position;
        Vector2 handPos = handObject.transform.position;
        Vector2 centerPos = new(0, 0);

        GameObject cardInstance = Instantiate(cardPrefab, deckPos, Quaternion.identity, hoverWarriorObject.transform);
        ObjectAnimation objectAnimation = cardInstance.GetComponentInChildren<ObjectAnimation>();
        Card card = cardInstance.GetComponentInChildren<Card>();

        card.SetStats(drawnCard);
        card.UpdateCardUi();

        cardInstance.transform.localScale = new Vector2(0.5f, 0.5f);
        await objectAnimation.MoveObject(deckPos, centerPos);
        cardInstance.transform.localScale = new Vector2(1, 1);
        await objectAnimation.MoveObject(centerPos, centerPos);
        cardInstance.transform.localScale = new Vector2(0.5f, 0.5f);
        await objectAnimation.MoveObject(centerPos, handPos);
        Destroy(cardInstance);

        hand.AddCardToHand(card);
    }

    void UpdateDeckUi() {
        textObject.GetComponent<TMP_Text>().text = $"{deck.Count}";
    }
}