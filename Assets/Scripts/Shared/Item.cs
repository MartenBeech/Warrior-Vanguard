using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public string title;
    public string displayTitle;
    public string description;
    public ItemManager.Rarity rarity;
    public GameObject image;
    private TooltipManager tooltipManager;

    public void SetItem(Item item) {
        title = item.title;
        displayTitle = item.displayTitle;
        description = item.description;
        rarity = item.rarity;
        UpdateItemUI();
    }

    public void UpdateItemUI() {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{title}");
    }

    public virtual void UseImmediately(ItemTriggerParams parameters) {
        // This metod should be overridden by each item
    }

    public virtual void UseOnWarriorSpawn(ItemTriggerParams parameters) {
        // This metod should be overridden by each item
    }

    public virtual void UseOnFriendSpawn(ItemTriggerParams parameters) {
        // This metod should be overridden by each item
    }

    public virtual void UseOnEnemySpawn(ItemTriggerParams parameters) {
        // This metod should be overridden by each item
    }
    public virtual async Task UseAfterFriendSpawn(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseAfterEnemySpawn(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseStartOfCombat(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseStartOfEnemyCombat(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseStartOfTurn(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseOnWarriorDeath(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseOnFriendDeath(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual async Task UseOnEnemyDeath(ItemTriggerParams parameters) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tooltipManager = FindFirstObjectByType<TooltipManager>();
        float tooltipWidth = tooltipManager.gameObject.GetComponent<RectTransform>().rect.width;

        // Make sure the tooltip does not go off-screen
        int screenWidth = Screen.width;
        float tooltipX = transform.position.x;
        if (tooltipX + (tooltipWidth / 2) > screenWidth) {
            tooltipX = screenWidth - (tooltipWidth / 2);
        } else if (tooltipX < tooltipWidth / 2) {
            tooltipX = tooltipWidth / 2;
        }

        int offsetY = transform.localScale.y <= 1 ? 100 : 165;

        tooltipManager.transform.position = new Vector2(tooltipX, transform.position.y - offsetY);
        tooltipManager.AddTooltip(displayTitle, description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        RemoveTooltips();
    }

    public void RemoveTooltips() {
        tooltipManager.RemoveTooltips();
    }

    public virtual Item GetItem() {
        return this;
        // This metod should be overridden by each item
    }
}