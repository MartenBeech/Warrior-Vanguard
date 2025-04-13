using UnityEngine;
using System.Collections.Generic;

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

    public static void SetDeck(List<Card> newDeck) {
        deck = new List<Card>(newDeck);
        SaveDeck();
    }

    public static Card GetCard(int index) {
        return deck[index];
    }

    public static List<WarriorStats> GetUnUpgradedCards() {
        List<WarriorStats> warriorStatsDeck = new();
        foreach (Card card in LoadDeck()) {
            if (card.stats.level == 0) {
                warriorStatsDeck.Add(card.stats);
            }
        }
        return warriorStatsDeck;
    }

    public static List<WarriorStats> GetDeck() {
        List<WarriorStats> warriorStatsDeck = new();
        foreach (Card card in LoadDeck()) {
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

    static List<Card> LoadDeck() {
        List<Card> tempDeck = new();
        if (!PlayerPrefs.HasKey(deckKey)) return tempDeck;

        string savedData = PlayerPrefs.GetString(deckKey, "");

        string[] statIds = savedData.Split(',');

        foreach (string id in statIds) {
            WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(id);
            if (stats != null) {
                GameObject newGameObject = new();
                newGameObject.AddComponent<Card>();
                Card card = newGameObject.GetComponent<Card>();
                card.SetStats(stats);
                tempDeck.Add(card);
            }
        }

        return tempDeck;
    }
}
