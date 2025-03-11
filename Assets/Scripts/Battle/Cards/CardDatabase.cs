using UnityEngine;
using System.Collections.Generic;

public class CardDatabase : MonoBehaviour {
    public List<WarriorStats> allCards = new();

    public static CardDatabase Instance;

    private void Awake() {
        allCards.Add(Duck.GetStats());
        allCards.Add(new BloodMerchant().GetStats());
        allCards.Add(new TheOriginal().GetStats());
        allCards.Add(new VampireApprentice().GetStats());
        allCards.Add(new VampireElder().GetStats());
        allCards.Add(new ZombieMinion().GetStats());
        allCards.Add(new PlagueWalker().GetStats());
        allCards.Add(new CorpseBehemoth().GetStats());
        allCards.Add(new ZombieHydra().GetStats());
        allCards.Add(Mario.GetStats());
        allCards.Add(Luigi.GetStats());
        allCards.Add(Mortana.GetStats());
        Instance = this;
    }

    public WarriorStats GetRandomWarriorStats() {
        return allCards[Rng.Range(0, allCards.Count)];
    }
}
