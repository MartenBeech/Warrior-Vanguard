using UnityEngine;
using TMPro;

public class TooltipHandler : MonoBehaviour
{
    public GameObject tooltipObject;
    public TextMeshProUGUI titleObject;
    public TextMeshProUGUI descriptionObject;

    public void ShowTooltip(string title, string description)
    {
        tooltipObject.SetActive(true);
        titleObject.text = title;
        descriptionObject.text = description;
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}