using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject attackText;
    public GameObject healthText;
    public GameObject costText;
    public GameObject titleText;
    public Image cardImage;
    public Sprite cardSprite;


    public string title = "";
    public int attack = 0;
    public int health = 0;
    public int cost = 0;

    public void DisplayCardUi()
    {
        titleText.GetComponent<TMP_Text>().text = $"{title}";
        attackText.GetComponent<TMP_Text>().text = $"{attack}";
        healthText.GetComponent<TMP_Text>().text = $"{health}";
        costText.GetComponent<TMP_Text>().text = $"{cost}";

        if (cardImage != null && cardSprite != null)
        {
            cardImage.sprite = cardSprite;
        }
    }

    public void CopyCardValues(Card from)
    {
        title = from.title;
        attack = from.attack;
        health = from.health;
        cost = from.cost;
        cardSprite = from.cardSprite;
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