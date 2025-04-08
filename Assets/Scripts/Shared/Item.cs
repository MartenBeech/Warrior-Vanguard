using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
    public string title;
    public string displayTitle;
    public string description;
    public GameObject image;

    public void UpdateItemUI() {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{displayTitle}");
    }

    public virtual void UseOnWarriorSpawn(WarriorStats stats) {
        // This metod should be overridden by each item
    }

    public virtual void UseImmediately() {
        // This metod should be overridden by each item
    }

    public virtual async Task UseAfterWarriorSpawn(WarriorStats stats, Vector2 gridIndex) {
        await Task.Delay(0); //This removes the CS1998 warning
        // This metod should be overridden by each item
    }
}