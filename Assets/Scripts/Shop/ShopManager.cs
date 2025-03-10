using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public List<Card> cardsForSale = new List<Card>();
    public CardDatabase cardDatabase;

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
        DeckManager.AddCard(card);
        
        cardsForSale.Remove(card);
        Destroy(card.gameObject);
    }

    public void ReturnToMap() {
        SceneLoader.LoadMap();
    }
}
