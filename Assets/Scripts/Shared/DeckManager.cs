using UnityEngine;
using System.Collections.Generic;

public static class DeckManager {
    public static List<Card> deck = new();

    public static void AddCard(Card card) {
        deck.Add(card);
    }

    public static void RemoveCard(int index) {
        if (deck.Count > index) {
            deck.RemoveAt(index);
        }
    }

    public static Card GetCard(int index) {
        return deck[index];
    }

    public static void SetDeck(List<Card> newDeck) {
        deck = new List<Card>(newDeck);
    }

    public static List<WarriorStats> GetDeck() {
        List<WarriorStats> warriorStatsDeck = new();
        foreach (Card card in deck) {
            warriorStatsDeck.Add(card.stats);
        }
        return warriorStatsDeck;
    }
}
