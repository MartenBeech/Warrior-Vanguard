using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Deck : MonoBehaviour {
    public Hand hand;
    public GameObject textObject;
    public GameObject handObject;
    public GameObject summonerObject;
    public GameObject cardPrefab;
    public WarriorSummoner.Alignment alignment;
    public List<WarriorStats> deckList = new();
    public GridManager gridManager;
    private int burnoutDamage = 0;

    public void GetDeck() {
        //If accessing this page from the Map, convert the Deck.
        deckList = DeckManager.GetDeck();
        ShuffleDeck(deckList);
        if (deckList.Count == 0) {
            FillDeckWithNewestCards();
        }

        UpdateDeckUi();
    }

    void FillDeckWithNewestCards() {
        for (int i = 0; i < CardDatabase.allCards.Count; i++) {
            deckList.Add(CardDatabase.allCards[i]);
        }
    }

    public async Task DrawCard(bool highlightCard = true) {
        if (deckList.Count == 0) {
            await BurnOut();
            return;
        }

        WarriorStats drawnCard = deckList[0];
        drawnCard.alignment = alignment;
        deckList.RemoveAt(0);

        UpdateDeckUi();

        Vector2 handPos = hand.GetPositionOfNextCardInHand();

        Vector2 deckPos = transform.position;
        Vector2 centerPos = new(920, 490);

        GameObject cardInstance = Instantiate(cardPrefab, deckPos, Quaternion.identity, transform);
        ObjectAnimation objectAnimation = cardInstance.GetComponentInChildren<ObjectAnimation>();
        Card card = cardInstance.GetComponentInChildren<Card>();

        List<Warrior> friends = gridManager.GetFriends(alignment);
        foreach (var friend in friends) {
            friend.stats.ability.enlighten.TriggerDraw(friend);
            friend.stats.ability.intelligence.TriggerDraw(friend);
            friend.stats.ability.summoningSpirits.TriggerDraw(drawnCard);
        }

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
        textObject.GetComponent<TMP_Text>().text = $"{deckList.Count}";
    }

    public async void OnClick() {
        await DrawCard();
    }

    public async Task BurnOut() {
        burnoutDamage++;

        Vector2 deckPos = transform.position;
        Vector2 summonerPos = summonerObject.transform.position;
        Vector2 centerPos = new(920, 490);

        GameObject cardInstance = Instantiate(cardPrefab, deckPos, Quaternion.identity, transform);
        ObjectAnimation objectAnimation = cardInstance.GetComponentInChildren<ObjectAnimation>();
        Card card = cardInstance.GetComponentInChildren<Card>();

        Burnout burnout = new();
        WarriorStats stats = burnout.GetStats();
        stats.cost[0] = burnoutDamage;
        card.SetStats(stats);
        card.UpdateCardUI();

        await objectAnimation.MoveObject(deckPos, centerPos);
        cardInstance.transform.localScale = new Vector2(2, 2);
        await objectAnimation.MoveObject(centerPos, centerPos);
        cardInstance.transform.localScale = new Vector2(1, 1);
        await objectAnimation.MoveObject(centerPos, summonerPos);

        Destroy(cardInstance);

        Summoner summoner = summonerObject.GetComponent<Summoner>();
        await summoner.TakeDamage(null, burnoutDamage, null, Warrior.DamageType.Physical);
    }

    public void ShuffleDeck(List<WarriorStats> deck) {
        int n = deck.Count;
        while (n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            (deck[n], deck[k]) = (deck[k], deck[n]);
        }
    }
}