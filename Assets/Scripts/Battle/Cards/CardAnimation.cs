using UnityEngine;
using System.Threading.Tasks;

public class CardAnimation : MonoBehaviour
{
    Vector2 fromVector;
    Vector2 toVector;
    float duration;
    float counter = 0;

    private void Update()
    {
        if (counter > 0)
        {
            Vector2 dir = toVector - fromVector;
            float dist = Mathf.Sqrt(
                Mathf.Pow(toVector.x - fromVector.x, 2) +
                Mathf.Pow(toVector.y - fromVector.y, 2));
            transform.Translate(dir.normalized * dist * Time.deltaTime / duration * Settings.gameSpeed);
            counter -= Time.deltaTime * Settings.gameSpeed;

            if (counter <= 0)
            {
                transform.position = toVector;
            }
        }
    }

    public async Task MoveCard(GameObject gameObject, Vector2 from, Vector2 to, float durationInSec = 1)
    {
        var card = gameObject.GetComponentInChildren<CardAnimation>();
        card.fromVector = from;
        card.toVector = to;
        card.duration = durationInSec;
        card.counter = durationInSec;

        await Task.Run(async () =>
        {
            while (card.counter > 0)
            {
                await Task.Yield();
            }
        });
    }
}