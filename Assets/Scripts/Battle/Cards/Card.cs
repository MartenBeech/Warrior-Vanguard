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
    public GameObject racePanel;
    public TMP_Text raceText;
    public GameObject rangeImage;
    public GameObject speedImage;
    public WarriorStats stats = new();
    HoverWarrior hoverWarrior;
    HoverWarrior hoverCardFromEvent;
    Hand hand;
    GameManager gameManager;
    bool isDisabled;

    public void UpdateCardUI() {
        costText.text = $"{stats.GetCost()}";
        Sprite sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");
        image.GetComponent<Image>().sprite = sprite != null ? sprite : Resources.Load<Sprite>($"Images/Icons/Red Cross");
        titleText.text = $"{stats.displayTitle}";
        if (stats.level == 1) {
            GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Icons/GoldenBackground");
        } else {
            GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Icons/SilverBackground");
        }

        if (stats.cardType == CardType.warrior) {
            attackText.text = $"{stats.GetStrength()}";
            healthText.text = $"{stats.GetHealth()}";
            raceText.text = $"{stats.race}";

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
            raceText.text = $"{stats.race}";
        }

        if (stats.race == Character.Race.None) {
            racePanel.SetActive(false);
            raceText.text = "";
        } else {
            racePanel.SetActive(true);
        }

        UpdateDisabledUI();
    }

    public void SetStats(WarriorStats stats) {
        this.stats.SetStats(stats);
    }

    public void SetHoverWarrior() {
        hoverWarrior = FindFirstObjectByType<HoverWarrior>();
    }

    public void SetHoverCardFromEvent() {
        hoverCardFromEvent = FindFirstObjectByType<HoverWarrior>();
    }

    public void SetGameManager() {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    public void OnClick() {
        if (hand == null) return;
        if (isDisabled) return;

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
        } else if (hoverCardFromEvent) {
            Vector2 position = transform.position;
            hoverCardFromEvent.ShowCardFromEvent(stats, position);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        HideCard();
    }

    public void HideCard() {
        if (hoverWarrior) {
            hoverWarrior.HideCard();
        } else if (hoverCardFromEvent) {
            Debug.Log("Hiding card from event");
            hoverCardFromEvent.HideCard();
        }
    }

    public void SetHand(Hand hand) {
        this.hand = hand;
    }

    public void UpdateDisabledUI() {
        isDisabled = false;
        if (gameManager && stats.alignment == CharacterSpawner.Alignment.Friend) {
            isDisabled = !gameManager.friendCoin.CanAfford(stats.GetCost());
        }

        Color color = GetComponent<Image>().color;
        color.a = isDisabled ? 0.25f : 1f;
        GetComponent<Image>().color = color;
    }
}