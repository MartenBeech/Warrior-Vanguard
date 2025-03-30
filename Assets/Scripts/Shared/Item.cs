using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public string title;
    public string description;
    public GameObject image;

    public void UpdateItemUI() {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{title}");
        Debug.Log($"Images/Items/{title}");
    }
}