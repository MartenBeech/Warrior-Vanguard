using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Deck : MonoBehaviour {
    public Hand hand;
    public GameObject textObject;
    public GameObject handObject;
    public GameObject cardPrefab;
    public CharacterSpawner.Alignment alignment;
    public List<WarriorStats> deck = new();

    public void GetDeck() {
        //If accessing this page from the Map, convert the Deck.
        deck = DeckManager.GetDeck();
        if (deck.Count == 0) {
            FillDeckWithRandomCards();
        }

        UpdateDeckUi();
    }

    void FillDeckWithRandomCards() {
        for (int i = 0; i < CardDatabase.allCards.Count; i++) {
            deck.Add(CardDatabase.allCards[i]);
        }
    }

    public async Task DrawCard(bool highlightCard = true) {
        if (deck.Count == 0) return;

        WarriorStats drawnCard = deck[0];
        drawnCard.alignment = alignment;
        deck.RemoveAt(0);

        UpdateDeckUi();

        Vector2 deckPos = transform.position;
        Vector2 handPos = handObject.transform.position;
        Vector2 centerPos = new(920, 490);

        GameObject cardInstance = Instantiate(cardPrefab, deckPos, Quaternion.identity, transform);
        ObjectAnimation objectAnimation = cardInstance.GetComponentInChildren<ObjectAnimation>();
        Card card = cardInstance.GetComponentInChildren<Card>();

        card.SetStats(drawnCard);
        card.UpdateCardUI();

        if (highlightCard) {
            await objectAnimation.MoveObject(deckPos, centerPos);
            cardInstance.transform.localScale = new Vector2(2, 2);
            await objectAnimation.MoveObject(centerPos, centerPos);
            cardInstance.transform.localScale = new Vector2(1, 1);
            await objectAnimation.MoveObject(centerPos, handPos);
        } else {
            await objectAnimation.MoveObject(deckPos, handPos);
        }

        Destroy(cardInstance);

        hand.AddCardToHand(card.stats);
    }

    void UpdateDeckUi() {
        textObject.GetComponent<TMP_Text>().text = $"{deck.Count}";
    }

    public async void OnClick() {
        await DrawCard();
    }
}