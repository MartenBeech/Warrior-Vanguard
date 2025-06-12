using UnityEngine;

public class HoverCard : MonoBehaviour {
    public GameObject hoverPanel;
    public GameObject cardObject;
    public TooltipManager tooltipManager;

    const float battlefieldCardOffset = 225;
    const float battlefieldCardTooltipOffset = 325;
    const float eventCardOffset = 275;
    const float eventCardTooltipOffset = 375;

    public void UpdateCardUI(WarriorStats stats) {
        Card card = cardObject.GetComponent<Card>();
        card.stats = stats;
        card.UpdateCardUI();
    }

    public void ShowCardFromBattlefield(WarriorStats stats, Vector2 position) {
        UpdateCardUI(stats);
        float battlefieldCardOffsetCopy = battlefieldCardOffset;
        float cardTooltipOffsetCopy = battlefieldCardTooltipOffset;
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
        tooltipManager.transform.position = new Vector2(960 + battlefieldCardTooltipOffset, 540);
        cardObject.SetActive(true);
        DisplayTooltips(stats);
    }

    public void ShowCardFromEvent(WarriorStats stats, Vector2 position) {
        UpdateCardUI(stats);
        float battlefieldCardOffsetCopy = eventCardOffset;
        float cardTooltipOffsetCopy = eventCardTooltipOffset;
        if (position.x > 960) {
            battlefieldCardOffsetCopy *= -1;
            cardTooltipOffsetCopy *= -1;
        }
        hoverPanel.transform.position = new Vector2(position.x + battlefieldCardOffsetCopy, position.y);
        tooltipManager.transform.position = new Vector2(position.x + battlefieldCardOffsetCopy + cardTooltipOffsetCopy, position.y);
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
