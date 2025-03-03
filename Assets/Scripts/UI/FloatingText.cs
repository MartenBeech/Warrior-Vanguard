using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class FloatingText : MonoBehaviour {
    public GameObject floatingTextObject;

    public async Task CreateFloatingText(Vector2 pos, string text, float durationInSec = 1) {
        var floatingText = Instantiate(floatingTextObject, pos, Quaternion.identity, transform);
        TextAnimation textAnimation = floatingText.GetComponentInChildren<TextAnimation>();
        textAnimation.GetComponent<TMP_Text>().text = text;
        ColorPalette color = new();
        textAnimation.GetComponent<TMP_Text>().color = color.GetColor(ColorPalette.ColorEnum.red);
        await textAnimation.SetupFloatingText(pos, durationInSec);
    }
}