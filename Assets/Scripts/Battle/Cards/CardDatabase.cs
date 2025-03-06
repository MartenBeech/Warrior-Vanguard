using UnityEngine;
using System.Collections.Generic;

public class CardDatabase : MonoBehaviour {
    public List<WarriorStats> allCards = new List<WarriorStats>();

    public static CardDatabase Instance;

    private void Awake() {
        allCards.Add(Mario.GetCard());
        allCards.Add(Luigi.GetCard());
        allCards.Add(Duck.GetCard());
        allCards.Add(Mortana.GetCard());
        Instance = this;
    }

    public WarriorStats GetRandomWarriorStats() {
        return allCards[Random.Range(0, allCards.Count)];
    }
}
