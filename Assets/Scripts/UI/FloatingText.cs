using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class FloatingText : MonoBehaviour {
    public GameObject floatingTextObject;

    public async Task CreateFloatingText(Transform transform, string text, float durationInSec = 1) {
        RectTransform rectTransform = transform as RectTransform;
        Vector2 pos = new Vector2(transform.position.x, transform.position.y + rectTransform.rect.height / 2);
        var floatingText = Instantiate(floatingTextObject, pos, Quaternion.identity, this.transform);
        TextAnimation textAnimation = floatingText.GetComponentInChildren<TextAnimation>();
        textAnimation.GetComponent<TMP_Text>().text = text;
        ColorPalette color = new();
        textAnimation.GetComponent<TMP_Text>().color = color.GetColor(ColorPalette.ColorEnum.red);
        textAnimation.GetComponent<RectTransform>().sizeDelta = new Vector2(rectTransform.rect.width / 2, rectTransform.rect.height / 2);
        await textAnimation.SetupFloatingText(pos, durationInSec);
    }
}