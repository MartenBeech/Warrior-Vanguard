using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    string shopCardsKey = "shopCards";
    public List<Card> cardsForSale = new List<Card>();
    public TMP_Text actionInfoText;
    public DeckBuilder deckBuilder;

    private void Start() {
        PopulateShop();
    }

    void PopulateShop() {
        if (PlayerPrefs.HasKey(shopCardsKey)) {
            LoadShop();
        } else {
            foreach (Card card in cardsForSale) {
                int randomIndex = Random.Range(0, CardDatabase.allCards.Count);
                WarriorStats stats = CardDatabase.allCards[randomIndex];
                card.SetStats(stats);
                card.UpdateCardUi();
            }

            SaveShop();
        }
    }

    public void BuyCard(Card card) {
        if (GoldManager.SpendGold(50)) {
            deckBuilder.AddCardToDeck(card);

            cardsForSale.Remove(card);
            Destroy(card.gameObject);
            SaveShop();
            actionInfoText.text = $"Added {card.stats.title} to your deck";
        } else {
            actionInfoText.text = $"Not enough gold!";
        }

    }

    private void SaveShop() {
        List<string> cardTitlesAndLevels = new();
        foreach (Card card in cardsForSale) {
            cardTitlesAndLevels.Add($"{card.stats.title}_{card.stats.level}");
        }

        string cardData = string.Join(",", cardTitlesAndLevels);
        PlayerPrefs.SetString(shopCardsKey, cardData);
        PlayerPrefs.Save();
    }

    private void LoadShop() {
        string cardData = PlayerPrefs.GetString(shopCardsKey);
        string[] cardTitlesAndLevels = cardData.Split(',');

        for (int i = 0; i < cardsForSale.Count; i++) {
            if (i >= cardTitlesAndLevels.Length) {
                // This happens if cards have already been bought, and then reloading the shop.
                cardsForSale[i].gameObject.SetActive(false);
                break;
            }
            
            WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(cardTitlesAndLevels[i]);
            cardsForSale[i].SetStats(stats);
            cardsForSale[i].UpdateCardUi();
        }
    }

    public void ReturnToMap() {
        PlayerPrefs.DeleteKey(shopCardsKey);
        TileCompleter.MarkTileAsCompleted();
        SceneLoader.LoadMap();
    }
}
