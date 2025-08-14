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
    public CharacterSpawner characterSpawner;
    public Summoner friendSummoner;


    public void OnPointerEnter(PointerEventData eventData) {
        tooltipManager.transform.position = new Vector2(transform.position.x, transform.position.y + 100);
        tooltipManager.AddTooltip(title, description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.RemoveTooltips();
    }

    public void SetHeroPower(string title, string description, int cost, Action<HeroPowerEffectParams> effect) {
        this.title = title;
        this.description = description;
        this.cost = cost;
        this.effect = effect;
        costText.text = $"{cost}";
    }

    public void HeroPowerClicked() {
        gameManager.friendCoin.SpendCoins(cost);
        heroPowerButton.interactable = false;
        effect(new(gameManager, friendHand, characterSpawner, friendSummoner));
    }

    public bool CanActivateHeroPower() {
        if (gameManager.friendCoin.CanAfford(cost)) {
            heroPowerButton.interactable = true;
            return true;
        }

        heroPowerButton.interactable = false;
        return false;
    }

    public void RefreshHeroPower() {
        heroPowerButton.interactable = CanActivateHeroPower();
    }
}