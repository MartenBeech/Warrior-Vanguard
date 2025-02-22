using UnityEngine;

public class Character : MonoBehaviour
{
    public void SetPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}