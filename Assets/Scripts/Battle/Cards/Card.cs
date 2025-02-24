using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    public GameObject attackText;
    public GameObject healthText;
    public GameObject costText;

    public int attack = 0;
    public int health = 0;
    public int cost = 0;

    public void DisplayCardUi() {
        attackText.GetComponent<TMP_Text>().text = $"{attack}";
        healthText.GetComponent<TMP_Text>().text = $"{health}";
        costText.GetComponent<TMP_Text>().text = $"{cost}";
    }

    public void CopyCardValues(Card from) {
        attack = from.attack;
        health = from.health;
        cost = from.cost;
    }
}