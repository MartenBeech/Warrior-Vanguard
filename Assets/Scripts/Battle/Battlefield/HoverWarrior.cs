using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HoverWarrior : MonoBehaviour {
    public GameObject hoverPanel;
    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text costText;
    public TMP_Text titleText;
    public GameObject image;

    public void DisplayCardUI(CardStats stats) {
        attackText.text = $"{stats.attack}";
        healthText.text = $"{stats.health}";
        costText.text = $"{stats.cost}";
        titleText.text = $"{stats.title}";
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");
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
