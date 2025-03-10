using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour {
    public TextMeshProUGUI titleObject;
    public TextMeshProUGUI descriptionObject;

    public void SetTooltip(string title, string description, float spawnDelay) {
        titleObject.text = title;
        descriptionObject.text = description;
        Invoke(nameof(ShowObject), spawnDelay);
    }

    void ShowObject() {
        gameObject.SetActive(true);
    }
}