using UnityEngine;
using System.Collections.Generic;

public static class DeckManager
{
    public static List<Card> selectedDeck = new();

    public static void SetDeck(List<Card> deck)
    {
        selectedDeck = new List<Card>(deck);
    }

    public static List<WarriorStats> GetDeck()
    {
        List<WarriorStats> warriorStatsDeck = new();
        foreach (Card card in selectedDeck)
        {
            warriorStatsDeck.Add(card.stats);
        }
        return warriorStatsDeck;
    }
}
