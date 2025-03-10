using UnityEngine;

public class HoverWarrior : MonoBehaviour {
    public GameObject hoverPanel;
    public GameObject cardObject;
    public TooltipManager tooltipManager;

    public void UpdateCardUI(WarriorStats stats) {
        Card card = cardObject.GetComponent<Card>();
        card.stats = stats;
        card.UpdateCardUi();
    }

    public void ShowCardFromBattlefield(WarriorStats stats, Vector2 position) {
        UpdateCardUI(stats);
        hoverPanel.transform.position = new Vector2(position.x + 200, position.y);
        cardObject.SetActive(true);
        DisplayTooltips(stats);
    }

    public void ShowCardFromHand(WarriorStats stats) {
        UpdateCardUI(stats);
        hoverPanel.transform.position = new Vector2(0, 0);
        cardObject.SetActive(true);
        DisplayTooltips(stats);
    }

    public void HideCard() {
        cardObject.SetActive(false);
    }

    public void DisplayTooltips(WarriorStats stats) {
        WarriorAbility warriorAbility = new();
        warriorAbility.DisplayAbilityTooltip(tooltipManager, stats);
    }
}
