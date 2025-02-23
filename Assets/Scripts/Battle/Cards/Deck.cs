using UnityEngine;
using TMPro;

public class Deck : MonoBehaviour
{
    public Hand hand;
    public CardAnimation cardAnimation;
    public GameObject deckObject;
    public GameObject textObject;
    public GameObject handObject;
    public GameObject cardPrefab;
    int deckSize = 50;

    public void DrawCard() {
        deckSize--;
        UpdateDeckUi();
        Vector2 deckPos = new Vector2(deckObject.transform.position.x, deckObject.transform.position.y);
        Vector2 handPos = new Vector2(handObject.transform.position.x, handObject.transform.position.y);
        GameObject card = Instantiate(cardPrefab, deckPos, Quaternion.identity, deckObject.transform);
        cardAnimation.MoveCard(card, deckPos, handPos, () => {
            Destroy(card);
            hand.AddCardToHand();
        });
    }

    void UpdateDeckUi() {
        textObject.GetComponent<TMP_Text>().text = $"{deckSize}";
    }
}