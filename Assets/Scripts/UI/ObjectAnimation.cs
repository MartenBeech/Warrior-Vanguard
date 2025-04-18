using UnityEngine;
using System.Threading.Tasks;

public class ObjectAnimation : MonoBehaviour {
    Vector2 fromVector;
    Vector2 toVector;
    float duration;
    float counter = 0;
    bool destroyAfterMoving;

    private void Update() {
        if (counter > 0) {
            Vector2 dir = toVector - fromVector;
            float dist = Mathf.Sqrt(
                Mathf.Pow(toVector.x - fromVector.x, 2) +
                Mathf.Pow(toVector.y - fromVector.y, 2));
            transform.Translate(dist * Time.deltaTime * dir.normalized / duration * Settings.gameSpeed);
            counter -= Time.deltaTime * Settings.gameSpeed;

            if (counter <= 0) {
                transform.position = toVector;
                if (destroyAfterMoving) {
                    Destroy(gameObject);
                }
            }
        }
    }

    public async Task MoveObject(Vector2 from, Vector2 to, float durationInSec = 1, bool destroyAfterMoving = false) {
        fromVector = from;
        toVector = to;
        duration = durationInSec;
        counter = durationInSec;
        this.destroyAfterMoving = destroyAfterMoving;

        await Task.Run(async () => {
            while (counter > 0) {
                await Task.Yield();
            }
        });
    }
}