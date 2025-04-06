using TMPro;
using UnityEngine;

public class GoldTextUpdater : MonoBehaviour {
    public TMP_Text goldText;

    void Update() {
        goldText.text = $"{GoldManager.gold} gold";
    }
}
