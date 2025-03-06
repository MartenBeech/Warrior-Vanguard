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
        attackText.text = $"{stats.attack}";
        healthText.text = $"{stats.health}";
        costText.text = $"{stats.cost}";
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");
        titleText.text = $"{stats.title}";
        WarriorAbility ability = new();
        abilityText.text = ability.GetAbilityText(this);
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