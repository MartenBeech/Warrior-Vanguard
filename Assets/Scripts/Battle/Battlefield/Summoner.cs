using UnityEngine;
using UnityEngine.EventSystems;

public class Summoner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string title;
    public string description;
    public TooltipHandler tooltipHandler;
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipHandler.ShowTooltip(title, description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipHandler.HideTooltip();
    }
}