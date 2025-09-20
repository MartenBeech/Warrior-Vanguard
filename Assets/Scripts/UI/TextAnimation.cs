using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;

public class TextAnimation : MonoBehaviour {
    Vector2 fromVector;
    Vector2 toVector;
    float duration;
    float counter = 0;
    public TMP_Text textObject;
    public GameObject backgroundImage;

    private void Update() {
        if (counter > 0) {
            Vector2 dir = toVector - fromVector;
            float dist = Mathf.Sqrt(
                Mathf.Pow(toVector.x - fromVector.x, 2) +
                Mathf.Pow(toVector.y - fromVector.y, 2));
            transform.Translate(dist * Time.deltaTime * dir.normalized / duration * Settings.gameSpeed);
            counter -= Time.deltaTime * Settings.gameSpeed;

            float transparency = counter * 150;
            textObject.color = ColorPalette.AddTransparency(textObject.color, transparency);
            if (backgroundImage) {
                backgroundImage.GetComponent<Image>().color = ColorPalette.AddTransparency(ColorPalette.GetColor(ColorEnum.White), transparency);
            }

            if (counter <= 0) {
                Destroy(gameObject);
            }
        }
    }

    public async Task SetupFloatingText(Vector2 pos, Sprite image) {
        fromVector = pos;
        toVector = new Vector2(pos.x, pos.y + 100);
        duration = 1;
        counter = duration;
        if (image == null) {
            backgroundImage = null;
        } else {
            backgroundImage.GetComponent<Image>().sprite = image;
        }

        await Task.Run(async () => {
            while (counter > 0) {
                await Task.Yield();
            }
        });
    }
}