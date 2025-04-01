using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text costText;
    public GameObject image;
    public TMP_Text titleText;
    public TMP_Text abilityText;
    public GameObject rangeImage;
    public GameObject speedImage;
    public WarriorStats stats = new();
    HoverWarrior hoverWarrior;
    Hand hand;

    public void UpdateCardUi() {
        costText.text = $"{stats.cost}";
        string cleanTitle = stats.title.Replace("+", string.Empty);
        cleanTitle = Regex.Replace(cleanTitle, "(?<!^)([A-Z])", " $1");
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{cleanTitle}");
        titleText.text = $"{cleanTitle}";

        if (stats.cardType == CardType.warrior) {
            attackText.text = $"{stats.GetStrength()}";
            healthText.text = $"{stats.GetHealth()}";

            if (stats.range == 2) {
                rangeImage.SetActive(false);
            } else {
                rangeImage.SetActive(true);
                rangeImage.GetComponentInChildren<TMP_Text>().text = stats.range.ToString();
            }
            if (stats.speed == 2) {
                speedImage.SetActive(false);
            } else {
                speedImage.SetActive(true);
                speedImage.GetComponentInChildren<TMP_Text>().text = stats.speed.ToString();
            }

            abilityText.text = stats.ability.GetAbilityText(stats);
        } else if (stats.cardType == CardType.spell) {
            abilityText.text = stats.spellDescription[stats.level];
            rangeImage.SetActive(false);
            speedImage.SetActive(false);
            attackText.text = "";
            healthText.text = "";
        }
    }

    public void SetStats(WarriorStats stats) {
        this.stats.SetStats(stats);
    }

    public void SetHoverWarrior() {
        hoverWarrior = FindFirstObjectByType<HoverWarrior>();
    }

    public void OnClick() {
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

    public void SetHand(Hand hand) {
        this.hand = hand;
    }
}