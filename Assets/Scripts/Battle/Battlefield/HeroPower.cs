using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

public class HeroPower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    string title;
    string description;
    int cost;
    Action<HeroPowerEffectParams> effect;
    public Button heroPowerButton;
    public TMP_Text costText;
    public TooltipManager tooltipManager;
    public GameManager gameManager;
    public Hand friendHand;
    public WarriorSummoner warriorSummoner;
    public Summoner friendSummoner;


    public void OnPointerEnter(PointerEventData eventData) {
        tooltipManager.transform.position = new Vector2(transform.position.x, transform.position.y + 100);
        tooltipManager.AddTooltip(title, description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.RemoveTooltips();
    }

    public void SetHeroPower(string title, string description, int cost, Sprite image, Action<HeroPowerEffectParams> effect) {
        this.title = title;
        this.description = description;
        this.cost = cost;
        this.effect = effect;
        costText.text = $"{cost}";
        GetComponent<Image>().sprite = image;

    }

    public void HeroPowerClicked() {
        gameManager.friendCoin.SpendCoins(cost);
        heroPowerButton.enabled = false;
        GetComponent<Image>().color = ColorPalette.AddTransparency(ColorPalette.GetColor(ColorEnum.White), 25);
        effect(new(gameManager, friendHand, warriorSummoner, friendSummoner));
    }

    public bool CanActivateHeroPower() {
        if (gameManager.friendCoin.CanAfford(cost)) {
            heroPowerButton.enabled = true;
            return true;
        }

        heroPowerButton.enabled = false;
        return false;
    }

    public void RefreshHeroPower() {
        if (gameManager.friendCoin.CanAfford(cost)) {
            heroPowerButton.enabled = true;
            GetComponent<Image>().color = ColorPalette.GetColor(ColorEnum.White);
            return;
        }

        heroPowerButton.enabled = false;
        GetComponent<Image>().color = ColorPalette.AddTransparency(ColorPalette.GetColor(ColorEnum.White), 25);
    }
}