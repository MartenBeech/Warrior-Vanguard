using UnityEngine;
using System.Collections.Generic;

public class CardDatabase : MonoBehaviour {
    public List<WarriorStats> allCards = new();

    public static CardDatabase Instance;

    private void Awake() {
        allCards.Add(ZombieMinion.GetStats());
        allCards.Add(PlagueWalker.GetStats());
        allCards.Add(CorpseBehemoth.GetStats());
        allCards.Add(ZombieHydra.GetStats());
        allCards.Add(Mario.GetStats());
        allCards.Add(Luigi.GetStats());
        allCards.Add(Duck.GetStats());
        allCards.Add(Mortana.GetStats());
        Instance = this;
    }

    public WarriorStats GetRandomWarriorStats() {
        return allCards[Rng.Range(0, allCards.Count)];
    }
}
