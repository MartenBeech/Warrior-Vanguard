using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class DeckManager {
    static string deckKey = "playerDeck";
    public static List<Card> deck = new();

    public static void AddCard(Card card) {
        deck = LoadDeck();
        deck.Add(card);
        SaveDeck();
    }

    public static void RemoveCard(int index) {
        deck = LoadDeck();
        if (deck.Count > index) {
            deck.RemoveAt(index);
        }
        SaveDeck();
    }

    public static void UpgradeCard(int index) {
        deck = LoadDeck();
        if (deck.Count > index) {
            deck[index].stats.level = 1;
        }
        SaveDeck();
    }

    public static Card GetCard(int index) {
        deck = LoadDeck();
        return deck[index];
    }

    public static List<Card> GetUnUpgradedCards() {
        List<Card> deck = LoadDeck();
        List<Card> unupgradedCards = deck.Where(card => card.stats.level == 0).ToList();

        return unupgradedCards;
    }

    public static List<int> GetDeckIndexes() {
        List<int> warriorStatsDeck = new();
        for (int i = 0; i < LoadDeck().Count; i++) {
            warriorStatsDeck.Add(i);
        }

        return warriorStatsDeck;
    }

    public static List<WarriorStats> GetDeck() {
        List<WarriorStats> warriorStatsDeck = new();
        List<Card> deck = LoadDeck();
        foreach (Card card in deck) {
            warriorStatsDeck.Add(card.stats);
        }
        return warriorStatsDeck;
    }

    public static void SaveDeck() {
        List<string> cardTitlesAndLevels = new();
        foreach (Card card in deck) {
            cardTitlesAndLevels.Add($"{card.stats.title}_{card.stats.level}");
        }
        string serialized = string.Join(",", cardTitlesAndLevels);
        PlayerPrefs.SetString(deckKey, serialized);
        PlayerPrefs.Save();
    }

    public static List<Card> LoadDeck() {
        List<Card> tempDeck = new();
        if (!PlayerPrefs.HasKey(deckKey)) return tempDeck;

        string savedData = PlayerPrefs.GetString(deckKey, "");

        string[] statIds = savedData.Split(',');

        foreach (string id in statIds) {
            WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(id);
            if (stats != null) {
                GameObject newGameObject = new();
                Card card = newGameObject.AddComponent<Card>();
                card.SetStats(stats);
                tempDeck.Add(card);

                Object.Destroy(newGameObject);
            }
        }

        return tempDeck;
    }
}
