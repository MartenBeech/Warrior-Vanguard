using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public GameObject attackText;
    public GameObject healthText;
    public GameObject costText;

    public int attack = 0;
    public int health = 0;
    public int cost = 0;
    private Hand hand;

    private void Start()
    {
        hand = FindFirstObjectByType<Hand>();
    }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        hand.SelectCard(this);
    }
}