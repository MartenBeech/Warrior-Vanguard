using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform handObject;
    public GameObject cardPrefab;
    public Card selectedCard;

    int handSize = 0;

    public void AddCardToHand(Card card)
    {
        if (handSize >= 10) return;

        Vector2 pos = new Vector2(0, 0);

        GameObject cardInstance = Instantiate(cardPrefab, pos, Quaternion.identity, handObject);
        Card cardHand = cardInstance.GetComponentInChildren<Card>();
        cardHand.CopyCardValues(card);
        cardHand.DisplayCardUi();

        handSize++;
    }

    public void SelectCard(Card card)
    {
        selectedCard = card;
        CharacterSpawner characterSpawner = FindFirstObjectByType<CharacterSpawner>();
        characterSpawner.ActivateSpawnFriend();
    }
}