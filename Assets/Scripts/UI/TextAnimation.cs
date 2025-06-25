using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class TextAnimation : MonoBehaviour {
    Vector2 fromVector;
    Vector2 toVector;
    float duration;
    float counter = 0;
    TMP_Text text;

    void Awake() {
        text = GetComponent<TMP_Text>();
    }

    private void Update() {
        if (counter > 0) {
            Vector2 dir = toVector - fromVector;
            float dist = Mathf.Sqrt(
                Mathf.Pow(toVector.x - fromVector.x, 2) +
                Mathf.Pow(toVector.y - fromVector.y, 2));
            transform.Translate(dist * Time.deltaTime * dir.normalized / duration * Settings.gameSpeed);
            counter -= Time.deltaTime * Settings.gameSpeed;

            text.color = ColorPalette.AddTransparency(text.color, (int)(counter * 150));

            if (counter <= 0) {
                Destroy(gameObject);
            }
        }
    }

    public async Task SetupFloatingText(Vector2 pos, float durationInSec = 1) {
        fromVector = pos;
        toVector = new Vector2(pos.x, pos.y + 100);
        duration = durationInSec;
        counter = durationInSec;

        await Task.Run(async () => {
            while (counter > 0) {
                await Task.Yield();
            }
        });
    }
}