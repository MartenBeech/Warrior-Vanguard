using UnityEngine;
using System.Collections.Generic;

public class CardDatabase : MonoBehaviour {
    public List<CardStats> allCards = new List<CardStats>();

    public static CardDatabase Instance;

    private void Awake() {
        allCards.Add(Mario.GetCard());
        allCards.Add(Luigi.GetCard());
        allCards.Add(Duck.GetCard());
        allCards.Add(Mortana.GetCard());
        Instance = this;
    }
}
