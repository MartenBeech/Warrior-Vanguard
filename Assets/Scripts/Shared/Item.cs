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

    public virtual void UseOnWarriorSpawn(WarriorStats stats) {
        // This metod should be overridden by each item
    }

    public virtual void UseImmediately(WarriorStats stats) {
        // This metod should be overridden by each item
    }
}