using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public TMP_Text attackText;
    public TMP_Text healthText;
    public TMP_Text costText;
    public GameObject image;
    public TMP_Text titleText;
    public Transform abilities;
    public GameObject abilityTextPrefab;
    public TMP_Text spellDescription;
    public GameObject racePanel;
    public TMP_Text raceText;
    public GameObject rangeImage;
    public GameObject speedImage;
    public GameObject rarityCrystal;
    public WarriorStats stats = new();
    HoverCard hoverCard;
    HoverCard hoverCardFromMap;
    HoverCard hoverCardFromCollection;
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

        foreach (Transform child in abilities) {
            Destroy(child.gameObject);
        }

        if (stats.cardType == CardType.Warrior) {
            spellDescription.text = "";

            attackText.text = $"{stats.GetStrength()}";
            healthText.text = $"{stats.GetHealthCurrent()}";
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

            List<string> abilityTexts = stats.ability.GetAbilityText(stats);
            foreach (var text in abilityTexts) {
                GameObject abilityText = Instantiate(abilityTextPrefab, abilities);
                abilityText.GetComponent<TMP_Text>().text = text;
            }

        } else if (stats.cardType == CardType.Spell) {
            spellDescription.text = stats.spellDescription[stats.level];
            rangeImage.SetActive(false);
            speedImage.SetActive(false);
            attackText.text = "";
            healthText.text = "";
            raceText.text = $"{stats.race}";
        }

        if (stats.race != Warrior.Race.None) {
            racePanel.SetActive(true);
            racePanel.GetComponent<Image>().color = ColorPalette.AddTransparency(stats.genre switch {
                Warrior.Genre.Human => ColorPalette.GetColor(ColorPalette.ColorEnum.Blue),
                Warrior.Genre.Forest => ColorPalette.GetColor(ColorPalette.ColorEnum.GreenDark),
                Warrior.Genre.Undead => ColorPalette.GetColor(ColorPalette.ColorEnum.Purple),
                Warrior.Genre.Underworld => ColorPalette.GetColor(ColorPalette.ColorEnum.Orange),
                Warrior.Genre.Light => ColorPalette.GetColor(ColorPalette.ColorEnum.Yellow),
                Warrior.Genre.Dark => ColorPalette.GetColor(ColorPalette.ColorEnum.Gray),
                Warrior.Genre.Fire => ColorPalette.GetColor(ColorPalette.ColorEnum.Red),
                Warrior.Genre.Nature => ColorPalette.GetColor(ColorPalette.ColorEnum.Teal),
                _ => ColorPalette.GetColor(ColorPalette.ColorEnum.Black),
            }, 80);

            if (stats.cardType == CardType.Spell) {
                raceText.color = ColorPalette.GetColor(ColorPalette.ColorEnum.Black);
            } else if (stats.cardType == CardType.Warrior) {
                raceText.color = ColorPalette.GetColor(ColorPalette.ColorEnum.White);
            }

        } else {
            racePanel.SetActive(false);
        }

        if (stats.rarity != CardRarity.None) {
            rarityCrystal.SetActive(true);

            rarityCrystal.GetComponent<Image>().color = stats.rarity switch {
                CardRarity.Common => ColorPalette.GetColor(ColorPalette.ColorEnum.White),
                CardRarity.Rare => ColorPalette.GetColor(ColorPalette.ColorEnum.TealMedium),
                CardRarity.Legendary => ColorPalette.GetColor(ColorPalette.ColorEnum.Orange),
                _ => ColorPalette.GetColor(ColorPalette.ColorEnum.Black),
            };

        } else {
            rarityCrystal.SetActive(false);
        }

        UpdateDisabledUI();
    }

    public void SetStats(WarriorStats stats) {
        this.stats.SetStats(stats);
    }

    public void SetHoverCard() {
        hoverCard = FindFirstObjectByType<HoverCard>();
    }

    public void SetHoverCardFromMap() {
        hoverCardFromMap = FindFirstObjectByType<HoverCard>();
    }

    public void SetHoverCardFromCollection() {
        hoverCardFromCollection = FindFirstObjectByType<HoverCard>();
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
        if (hoverCard) {
            hoverCard.ShowCardFromHand(stats);
        } else if (hoverCardFromMap) {
            Vector2 position = transform.position;
            hoverCardFromMap.ShowCardFromEvent(stats, position);
        } else if (hoverCardFromCollection) {
            Vector2 position = transform.position;
            hoverCardFromCollection.ShowCardFromCollection(stats, position);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        HideCard();
    }

    public void HideCard() {
        if (hoverCard) {
            hoverCard.HideCard();
        } else if (hoverCardFromMap) {
            hoverCardFromMap.HideCard();
        } else if (hoverCardFromCollection) {
            hoverCardFromCollection.HideCard();
        }
    }

    public void SetHand(Hand hand) {
        this.hand = hand;
    }

    public void UpdateDisabledUI() {
        isDisabled = false;
        if (gameManager && stats.alignment == WarriorSummoner.Alignment.Friend) {
            isDisabled = !gameManager.friendCoin.CanAfford(stats.GetCost());
        }

        GetComponent<Image>().color = ColorPalette.AddTransparency(GetComponent<Image>().color, isDisabled ? 25 : 100);
    }
}