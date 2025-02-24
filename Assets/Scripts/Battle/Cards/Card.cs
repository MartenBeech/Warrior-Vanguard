using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject attackText;
    public GameObject healthText;
    public GameObject costText;
    public GameObject image;

    public int attack = 0;
    public int health = 0;
    public int cost = 0;
    public string title = "";

    public void DisplayCardUi()
    {
        if (attackText) attackText.GetComponent<TMP_Text>().text = $"{attack}";
        if (healthText) healthText.GetComponent<TMP_Text>().text = $"{health}";
        if (costText) costText.GetComponent<TMP_Text>().text = $"{cost}";
        if (image) image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/{title}");
    }

    public void CopyCardValues(Card from)
    {
        attack = from.attack;
        health = from.health;
        cost = from.cost;
        title = from.title;
    }

    public void OnClick()
    {
        Hand hand = FindFirstObjectByType<Hand>();
        if (this == hand.selectedCard)
        {
            hand.DeselectCard(hand.selectedCard);
        }
        else
        {
            hand.DeselectCard(hand.selectedCard);
            hand.SelectCard(this);
        }
    }
}