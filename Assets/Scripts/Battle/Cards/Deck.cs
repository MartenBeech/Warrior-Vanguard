using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    public Hand hand;
    public GameObject deck;
    public GameObject text;
    int deckSize = 50;

    public void DrawCard() {
        deckSize--;
        UpdateDeckUi();
        hand.AddCardToHand();
    }

    void UpdateDeckUi() {
        text.GetComponent<TMP_Text>().text = $"{deckSize}";
    }
}