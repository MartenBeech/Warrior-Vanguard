using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    public Vector2 fromVector;
    public Vector2 toVector;
    public float counter = 0;
    public UnityEngine.Events.UnityAction onComplete;

    private void Update() {
        if (counter > 0) {
            Vector3 dir = toVector - fromVector;
            float dist = Mathf.Sqrt(
                Mathf.Pow(toVector.x - fromVector.x, 2) +
                Mathf.Pow(toVector.y - fromVector.y, 2));
            transform.Translate(dir.normalized * dist * Time.deltaTime);
            counter -= Time.deltaTime;

            if (counter <= 0) {
                onComplete();
            }
        }
    }

    public void MoveCard(GameObject gameObject, Vector2 from, Vector2 to, UnityEngine.Events.UnityAction onComplete) {
        gameObject.GetComponentInChildren<CardAnimation>().fromVector = from;
        gameObject.GetComponentInChildren<CardAnimation>().toVector = to;
        gameObject.GetComponentInChildren<CardAnimation>().counter = 1;
        gameObject.GetComponentInChildren<CardAnimation>().onComplete = onComplete;
    }
}