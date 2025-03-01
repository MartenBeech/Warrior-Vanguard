using UnityEngine;

public class CreditsScroll : MonoBehaviour {
    public RectTransform creditsText;
    public float scrollSpeed = 50f;

    private float startY;
    private float endY;

    void Start() {
        startY = creditsText.anchoredPosition.y;
        endY = startY + 5000;
    }

    void Update() {
        if (creditsText.anchoredPosition.y < endY) {
            creditsText.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);
        }
    }
}
