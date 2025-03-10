using UnityEngine;

public class HoverWarrior : MonoBehaviour {
    public GameObject hoverPanel;
    public GameObject cardObject;
    public TooltipManager tooltipManager;

    float battlefieldCardOffset = 200;
    float cardTooltipOffset = 300;

    public void UpdateCardUI(WarriorStats stats) {
        Card card = cardObject.GetComponent<Card>();
        card.stats = stats;
        card.UpdateCardUi();
    }

    public void ShowCardFromBattlefield(WarriorStats stats, Vector2 position) {
        UpdateCardUI(stats);
        hoverPanel.transform.position = new Vector2(position.x + battlefieldCardOffset, position.y);
        tooltipManager.transform.position = new Vector2(position.x + battlefieldCardOffset + cardTooltipOffset, position.y);
        cardObject.SetActive(true);
        DisplayTooltips(stats);
    }

    public void ShowCardFromHand(WarriorStats stats) {
        UpdateCardUI(stats);
        hoverPanel.transform.position = new Vector2(0, 0);
        tooltipManager.transform.position = new Vector2(cardTooltipOffset, 0);
        cardObject.SetActive(true);
        DisplayTooltips(stats);
    }

    public void HideCard() {
        cardObject.SetActive(false);
        tooltipManager.RemoveTooltips();
    }

    public void DisplayTooltips(WarriorStats stats) {
        stats.ability.DisplayAbilityTooltip(tooltipManager);
    }
}
