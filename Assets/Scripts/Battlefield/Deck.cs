using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{
    public GameObject deck;
    public GameObject text;
    int deckSize = 50;

    public void DrawCard() {
        deckSize--;
        UpdateDeckUi();
    }

    void UpdateDeckUi() {
        text.GetComponent<TMPro.TextMeshProUGUI>().text = $"{deckSize}";
    }
}