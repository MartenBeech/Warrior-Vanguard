using UnityEngine;

public class CardStats
{
    // Title should be the same as the image name
    public string title = "";
    public int attack = 0;
    public int health = 0;
    public int cost = 0;

    public void CopyCardValues(CardStats from)
    {
        title = from.title;
        attack = from.attack;
        health = from.health;
        cost = from.cost;
    }
}