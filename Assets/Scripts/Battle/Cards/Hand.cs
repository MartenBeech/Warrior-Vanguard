using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;

    int handSize = 0;

    public void AddCardToHand() {
        if (handSize >= 10) return;
        
        Vector2 pos = new Vector2(0,0);
        Quaternion rot = Quaternion.identity;

        Instantiate(prefab, pos, rot, parent);
        handSize++;
    }
}