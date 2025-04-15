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

    public virtual void UseOnWarriorSpawn(WarriorStats stats) {
        // This metod should be overridden by each item
    }

    public virtual void UseImmediately() {
        // This metod should be overridden by each item
    }

    public virtual async Task UseAfterWarriorSpawn(WarriorStats stats, Vector2 gridIndex) {
        await Task.Delay(0); // This removes the CS1998 warning
        // This metod should be overridden by each item
    }

    public virtual void UseStartOfCombat(Summoner summoner) {
        // This metod should be overridden by each item
    }

    public virtual void UseStartOfTurn(Summoner summoner) {
        // This metod should be overridden by each item
    }

    public virtual void UseOnWarriorDeath(Summoner summoner) {
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