using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    string shopCardsKey = "shopCards";
    string shopItemsKey = "shopItems";
    public List<Card> cardsForSale = new List<Card>();
    public List<Item> itemsForSale = new List<Item>();
    public TMP_Text actionInfoText;
    public DeckBuilder deckBuilder;

    private void Start() {
        PopulateShop();
    }

    void PopulateShop() {
        if (PlayerPrefs.HasKey(shopCardsKey) && PlayerPrefs.HasKey(shopItemsKey)) {
            LoadShop();
        } else {
            List<WarriorStats> tempCards = new List<WarriorStats>(CardDatabase.allCards);
            foreach (Card card in cardsForSale) {
                int randomIndex = Random.Range(0, tempCards.Count);
                WarriorStats stats = tempCards[randomIndex];
                card.SetStats(stats);
                card.UpdateCardUi();
                tempCards.RemoveAt(randomIndex);
            }

            List<Item> tempItems = new List<Item>(ItemManager.availableItems);
            foreach (Item itemForSale in itemsForSale) {
                int randomIndex = Random.Range(0, tempItems.Count);
                Item item = tempItems[randomIndex];
                itemForSale.SetItem(item);
                tempItems.RemoveAt(randomIndex);
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
            actionInfoText.text = $"Added {card.stats.displayTitle} to your deck";
        } else {
            actionInfoText.text = $"Not enough gold!";
        }

    }

    public void BuyItem(Item item) {
        if (GoldManager.SpendGold(50)) {
            ItemManager.AddItem(item);

            itemsForSale.Remove(item);
            Destroy(item.gameObject);
            SaveShop();
            actionInfoText.text = $"Bought {item.displayTitle}";
        } else {
            actionInfoText.text = $"Not enough gold!";
        }
    }

    private void SaveShop() {
        List<string> cardTitlesAndLevels = new();
        List<string> itemTitles = new();
        foreach (Card card in cardsForSale) {
            cardTitlesAndLevels.Add($"{card.stats.title}_{card.stats.level}");
        }

        foreach (Item item in itemsForSale) {
            itemTitles.Add($"{item.title}");
        }

        string cardData = string.Join(",", cardTitlesAndLevels);
        PlayerPrefs.SetString(shopCardsKey, cardData);

        string itemData = string.Join(",", itemTitles);
        PlayerPrefs.SetString(shopItemsKey, itemData);

        PlayerPrefs.Save();
    }

    private void LoadShop() {
        string cardData = PlayerPrefs.GetString(shopCardsKey);
        string[] cardTitlesAndLevels = cardData.Split(',');

        for (int i = 0; i < cardsForSale.Count; i++) {
            if (i >= cardTitlesAndLevels.Length) {
                // This happens if cards have already been bought, and then reloading the shop.
                cardsForSale[i].gameObject.SetActive(false);
            } else {
                WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(cardTitlesAndLevels[i]);
                cardsForSale[i].SetStats(stats);
                cardsForSale[i].UpdateCardUi();
            }
        }

        string itemData = PlayerPrefs.GetString(shopItemsKey);
        string[] itemTitles = itemData.Split(',');

        for (int i = 0; i < itemsForSale.Count; i++) {
            Debug.Log(i);
            if (i >= itemTitles.Length) {
                Debug.Log("INVALID INDEX");
                // This happens if items have already been bought, and then reloading the shop.
                itemsForSale[i].gameObject.SetActive(false);
            } else {
                Item item = ItemManager.GetItemByTitle(itemTitles[i]);
                itemsForSale[i].SetItem(item);
            }
        }
    }

    public void ReturnToMap() {
        PlayerPrefs.DeleteKey(shopCardsKey);
        TileCompleter.MarkTileAsCompleted();
        SceneLoader.LoadMap();
    }
}
