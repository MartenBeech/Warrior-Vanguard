using UnityEngine;

public class TooltipManager : MonoBehaviour {
    public GameObject tooltipPrefab;

    public void AddTooltip(string title, string description, float spawnDelay = 0f) {
        GameObject tooltipObject = Instantiate(tooltipPrefab, transform);
        Tooltip tooltip = tooltipObject.GetComponent<Tooltip>();
        tooltip.SetTooltip(title, description, spawnDelay);
    }

    public void RemoveTooltips() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }
}