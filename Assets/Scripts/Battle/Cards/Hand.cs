using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform handObject;

    int handSize = 0;

    public void AddCardToHand(GameObject cardObject) {
        if (handSize >= 10) return;
        
        Vector2 pos = new Vector2(0,0);
        Quaternion rot = Quaternion.identity;

        Instantiate(cardObject, pos, rot, handObject);
        handSize++;
    }
}