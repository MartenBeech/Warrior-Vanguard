using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour {
    public TextMeshProUGUI titleObject;
    public TextMeshProUGUI descriptionObject;

    public void SetTooltip(string title, string description) {
        titleObject.text = title;
        descriptionObject.text = description;
    }
}