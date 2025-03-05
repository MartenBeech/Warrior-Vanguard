using UnityEngine;

public class HoverWarrior : MonoBehaviour {
    public GameObject hoverPanel;
    public GameObject cardObject;

    public void UpdateCardUI(WarriorStats stats) {
        Card card = cardObject.GetComponent<Card>();
        card.stats = stats;
        card.UpdateCardUi();
    }

    public void ShowCard(Vector2 position) {
        // Show the card on the right side of the warrior
        hoverPanel.transform.position = new Vector2(position.x + 200, position.y);
        hoverPanel.SetActive(true);
    }

    public void HideCard() {
        hoverPanel.SetActive(false);
    }
}
