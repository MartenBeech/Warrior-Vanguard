using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text costText;
    public GameObject image;
    public TMP_Text titleText;
    public TMP_Text abilityText;
    public WarriorStats stats = new();
    HoverWarrior hoverWarrior;

    public void UpdateCardUi() {
        attackText.text = $"{stats.strength}";
        healthText.text = $"{stats.health}";
        costText.text = $"{stats.cost}";
        string cleanTitle = stats.title.Replace("+", string.Empty);
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{cleanTitle}");
        titleText.text = $"{stats.title}";

        string abilities = "";
        if (stats.range != 2) {
            abilities += $"Range: {stats.range}\n";
        }
        if (stats.speed != 2) {
            abilities += $"Speed: {stats.speed}\n";
        }
        abilities += stats.ability.GetAbilityText();
        abilityText.text = abilities;
    }

    public void SetStats(WarriorStats stats) {
        this.stats.SetStats(stats);
    }

    public void SetHoverWarrior() {
        hoverWarrior = FindFirstObjectByType<HoverWarrior>();
    }

    public void OnClick() {
        Hand hand = FindFirstObjectByType<Hand>();
        if (this == hand.selectedCard) {
            hand.DeselectCard(hand.selectedCard);
        } else {
            hand.DeselectCard(hand.selectedCard);
            hand.SelectCard(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.ShowCardFromHand(stats);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.HideCard();
        }
    }
}