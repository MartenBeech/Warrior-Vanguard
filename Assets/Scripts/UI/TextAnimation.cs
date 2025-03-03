using UnityEngine;
using System.Threading.Tasks;

public class TextAnimation : MonoBehaviour {
    Vector2 fromVector;
    Vector2 toVector;
    float duration;
    float counter = 0;

    private void Update() {
        if (counter > 0) {
            Vector2 dir = toVector - fromVector;
            float dist = Mathf.Sqrt(
                Mathf.Pow(toVector.x - fromVector.x, 2) +
                Mathf.Pow(toVector.y - fromVector.y, 2));
            transform.Translate(dir.normalized * dist * Time.deltaTime / duration * Settings.gameSpeed);
            counter -= Time.deltaTime * Settings.gameSpeed;

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