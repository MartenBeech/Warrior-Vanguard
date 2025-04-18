using UnityEngine;

public class HoverWarrior : MonoBehaviour {
    public GameObject hoverPanel;
    public GameObject cardObject;
    public TooltipManager tooltipManager;

    const float battlefieldCardOffset = 225;
    const float cardTooltipOffset = 325;

    public void UpdateCardUI(WarriorStats stats) {
        Card card = cardObject.GetComponent<Card>();
        card.stats = stats;
        card.UpdateCardUi();
    }

    public void ShowCardFromBattlefield(WarriorStats stats, Vector2 position) {
        UpdateCardUI(stats);
        float battlefieldCardOffsetCopy = battlefieldCardOffset;
        float cardTooltipOffsetCopy = cardTooltipOffset;
        if (position.x > 960) {
            battlefieldCardOffsetCopy *= -1;
            cardTooltipOffsetCopy *= -1;
        }
        hoverPanel.transform.position = new Vector2(position.x + battlefieldCardOffsetCopy, position.y);
        tooltipManager.transform.position = new Vector2(position.x + battlefieldCardOffsetCopy + cardTooltipOffsetCopy, position.y);
        cardObject.SetActive(true);
        DisplayTooltips(stats);
    }

    public void ShowCardFromHand(WarriorStats stats) {
        UpdateCardUI(stats);
        hoverPanel.transform.position = new Vector2(960, 540);
        tooltipManager.transform.position = new Vector2(960 + cardTooltipOffset, 540);
        cardObject.SetActive(true);
        DisplayTooltips(stats);
    }

    public void HideCard() {
        cardObject.SetActive(false);
        tooltipManager.RemoveTooltips();
    }

    public void DisplayTooltips(WarriorStats stats) {
        stats.ability.DisplayAbilityTooltip(tooltipManager, stats);
    }
}
