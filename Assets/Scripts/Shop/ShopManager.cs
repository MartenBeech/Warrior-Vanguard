using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public List<Card> cardsForSale = new List<Card>();
    public CardDatabase cardDatabase;
    public TMP_Text actionInfoText;

    private void Start() {
        PopulateShop();
    }

    void PopulateShop() {
        foreach (Card card in cardsForSale) {
            int randomIndex = Random.Range(0, CardDatabase.Instance.allCards.Count);
            WarriorStats stats = CardDatabase.Instance.allCards[randomIndex];
            card.SetStats(stats);
            card.UpdateCardUi();
        }
    }

    public void BuyCard(Card card) {
        if (GoldManager.SpendGold(50)) {
            DeckManager.AddCard(card);

            cardsForSale.Remove(card);
            Destroy(card.gameObject);
            actionInfoText.text = $"Added {card.stats.title} to your deck";
        } else {
            actionInfoText.text = $"Not enough gold!";
        }

    }

    public void ReturnToMap() {
        SceneLoader.LoadMap();
    }
}
