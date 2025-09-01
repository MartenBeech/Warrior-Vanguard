using UnityEngine;

public class AnimationHelper {
    //Creates a smooth fade-in effect for the gameObject

    public System.Collections.IEnumerator FadeInObject(GameObject gameObject, float duration) {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
        gameObject.SetActive(true);

        float elapsed = 0f;
        while (elapsed < duration) {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }
}