using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    public Hand hand;
    public GameObject deckObject;
    public GameObject textObject;
    public GameObject handObject;
    public GameObject cardPrefab;
    public List<CardStats> deck = new List<CardStats>();

    private void Start()
    {
        FillDeckWithRandomCards();
        UpdateDeckUi();
    }

    void FillDeckWithRandomCards()
    {
        if (CardDatabase.Instance == null)
        {
            Debug.LogError("CardDatabase Instance not found!");
            return;
        }

        for (int i = 0; i < CardDatabase.Instance.allCards.Count; i++)
        {
            deck.Add(CardDatabase.Instance.allCards[i]);
        }
    }
    public async void DrawCard()
    {
        if (deck.Count == 0) return;

        CardStats drawnCard = deck[0];
        deck.RemoveAt(0);

        UpdateDeckUi();

        Vector2 deckPos = new Vector2(deckObject.transform.position.x, deckObject.transform.position.y);
        Vector2 handPos = new Vector2(handObject.transform.position.x, handObject.transform.position.y);
        Vector2 centerPos = new Vector2(0, 0);

        GameObject cardInstance = Instantiate(cardPrefab, deckPos, Quaternion.identity, deckObject.transform);
        CardAnimation cardAnimation = cardInstance.GetComponentInChildren<CardAnimation>();
        Card card = cardInstance.GetComponentInChildren<Card>();

        card.stats.attack = drawnCard.attack;
        card.stats.health = drawnCard.health;
        card.stats.cost = drawnCard.cost;
        card.stats.title = drawnCard.title;

        card.DisplayCardUi();

        // Animate card movement
        await cardAnimation.MoveCard(cardInstance, deckPos, centerPos);
        cardInstance.transform.localScale = new Vector2(2, 2);
        await cardAnimation.MoveCard(cardInstance, centerPos, centerPos);
        cardInstance.transform.localScale = new Vector2(1, 1);
        await cardAnimation.MoveCard(cardInstance, centerPos, handPos);

        Destroy(cardInstance);

        hand.AddCardToHand(card);
    }

    void UpdateDeckUi()
    {
        textObject.GetComponent<TMP_Text>().text = $"{deck.Count}";
    }
}