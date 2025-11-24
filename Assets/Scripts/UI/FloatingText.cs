using UnityEngine;
using System.Threading.Tasks;

public class FloatingText : MonoBehaviour {
    public GameObject floatingTextObject;

    public async Task CreateFloatingText(Transform transform, string text, ColorEnum textColor = ColorEnum.Red, bool smallText = true, Sprite backgroundImage = null) {
        RectTransform rectTransform = transform as RectTransform;
        Vector2 pos = new(transform.position.x, transform.position.y + rectTransform.rect.height / 2);
        var floatingText = Instantiate(floatingTextObject, pos, Quaternion.identity, this.transform);
        TextAnimation textAnimation = floatingText.GetComponentInChildren<TextAnimation>();
        textAnimation.textObject.text = text;
        textAnimation.textObject.color = ColorPalette.GetColor(textColor);
        if (smallText) {
            textAnimation.textObject.fontSize /= 2;
            textAnimation.textObject.enableAutoSizing = false;
        }
        await textAnimation.SetupFloatingText(pos, backgroundImage);
    }
}