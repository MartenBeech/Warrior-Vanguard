using UnityEngine;

public class TooltipManager : MonoBehaviour {
    public GameObject tooltipPrefab;
    public Transform tooltipsObject;

    public void AddTooltip(string title, string description) {
        GameObject tooltipObject = Instantiate(tooltipPrefab, tooltipsObject);
        Tooltip tooltip = tooltipObject.GetComponent<Tooltip>();
        tooltip.SetTooltip(title, description);
    }

    public void RemoveTooltips() {
        foreach (Transform child in tooltipsObject.transform) {
            Destroy(child.gameObject);
        }
    }
}