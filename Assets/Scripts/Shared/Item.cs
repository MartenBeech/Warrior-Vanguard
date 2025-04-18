using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string title;
    public string displayTitle;
    public string description;
    public GameObject image;
    private TooltipManager tooltipManager;

    public void UpdateItemUI() {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{displayTitle}");
    }

    public virtual void UseImmediately() {
        // This metod should be overridden by each item
    }

    public virtual void UseOnFriendSpawn(WarriorStats stats) {
        // This metod should be overridden by each item
    }

    public virtual async Task UseAfterFriendSpawn(WarriorStats stats, Vector2 gridIndex) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual void UseOnEnemySpawn(WarriorStats stats) {
        // This metod should be overridden by each item
    }

    public virtual async Task UseAfterEnemySpawn(WarriorStats stats, Vector2 gridIndex) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseStartOfCombat(Summoner summoner) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseStartOfTurn(Summoner summoner) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseOnWarriorDeath(Summoner summoner) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltipManager = FindFirstObjectByType<TooltipManager>();
        float tooltipWidth = tooltipManager.gameObject.GetComponent<RectTransform>().rect.width;
        tooltipManager.transform.position = new Vector2((tooltipWidth / 2) + 20, transform.position.y - 125);
        tooltipManager.AddTooltip(displayTitle, description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.RemoveTooltips();
    }

    public virtual Item GetItem() {
        return this;
        // This metod should be overridden by each item
    }
}