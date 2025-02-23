using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    public Hand hand;
    public GameObject deckObject;
    public GameObject textObject;
    public GameObject handObject;
    public GameObject cardPrefab;
    int deckSize = 50;

    public async void DrawCard() {
        deckSize--;
        UpdateDeckUi();
        Vector2 deckPos = new Vector2(deckObject.transform.position.x, deckObject.transform.position.y);
        Vector2 handPos = new Vector2(handObject.transform.position.x, handObject.transform.position.y);
        Vector2 centerPos = new Vector2(0, 0);
        GameObject cardInstance = Instantiate(cardPrefab, deckPos, Quaternion.identity, deckObject.transform);

        CardAnimation cardAnimation = cardInstance.GetComponentInChildren<CardAnimation>();
        Card card = cardInstance.GetComponentInChildren<Card>();
        card.attackText.GetComponent<TMP_Text>().text = $"{deckSize}";
        card.healthText.GetComponent<TMP_Text>().text = $"{deckSize}";
        
        await cardAnimation.MoveCard(cardInstance, deckPos, centerPos);
        cardInstance.transform.localScale = new Vector2(2, 2);
        await cardAnimation.MoveCard(cardInstance, centerPos, centerPos);
        cardInstance.transform.localScale = new Vector2(1, 1);
        await cardAnimation.MoveCard(cardInstance, centerPos, handPos);

        hand.AddCardToHand(cardInstance);
        Destroy(cardInstance);
    }

    void UpdateDeckUi() {
        textObject.GetComponent<TMP_Text>().text = $"{deckSize}";
    }
}