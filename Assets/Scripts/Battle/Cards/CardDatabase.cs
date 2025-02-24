using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CardData
{
    public string title;
    public int attack;
    public int health;
    public int cost;
    public Sprite cardImage;
}

public class CardDatabase : MonoBehaviour
{
    public List<CardData> allCards = new List<CardData>();

    public static CardDatabase Instance;

    private void Awake()
    {
        Instance = this;
    }
}
