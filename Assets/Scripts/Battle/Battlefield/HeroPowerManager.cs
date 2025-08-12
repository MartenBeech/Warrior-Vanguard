using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;

public class HeroPowerManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    string title;
    string description;
    int cost;
    public GameManager gameManager;
    public Button heroPowerButton;
    public TMP_Text costText;
    public TooltipManager tooltipManager;

    public void OnPointerEnter(PointerEventData eventData) {
        Debug.Log("Pointer entered hero power");
        tooltipManager.transform.position = new Vector2(transform.position.x, transform.position.y + 200);
        tooltipManager.AddTooltip(title, description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.RemoveTooltips();
    }

    public void SetHeroPower(string title, string description, int cost) {
        this.title = title;
        this.description = description;
        this.cost = cost;
        costText.text = $"{cost}";
    }

    //Button onClick can't access async methods directly, so we use this method to call the async method
    public void ActivateHeroPower() {
        _ = UseHeroPower();
    }

    private async Task UseHeroPower() {
        gameManager.friendCoin.SpendCoins(cost);
        heroPowerButton.interactable = false;
        await gameManager.friendSummoner.AddShield(cost);
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