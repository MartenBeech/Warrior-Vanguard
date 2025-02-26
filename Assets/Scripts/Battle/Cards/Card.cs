using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public GameObject attackText;
    public GameObject healthText;
    public GameObject costText;
    public GameObject image;
    public GameObject titleText;
    public CardStats stats = new();

    public void DisplayCardUi()
    {
        if (attackText) attackText.GetComponent<TMP_Text>().text = $"{stats.attack}";
        if (healthText) healthText.GetComponent<TMP_Text>().text = $"{stats.health}";
        if (costText) costText.GetComponent<TMP_Text>().text = $"{stats.cost}";
        if (image) image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");
        if (titleText) titleText.GetComponent<TMP_Text>().text = $"{stats.title}";
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