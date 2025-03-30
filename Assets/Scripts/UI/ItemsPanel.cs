using UnityEngine;

public class ItemsPanel : MonoBehaviour {
    public Transform itemListContainer;
    public GameObject itemPrefab;

    void Start() {
    ItemManager.ItemsPanel = this;
    UpdateUI();
}

    public void UpdateUI() {
        foreach (Transform child in itemListContainer) {
            Destroy(child.gameObject);
        }

        foreach (Item item in ItemManager.items) {
            GameObject newItem = Instantiate(itemPrefab, itemListContainer);
            Item itemComponent = newItem.GetComponent<Item>();

            if (itemComponent != null) {
                itemComponent.title = item.title;
                itemComponent.description = item.description;
                itemComponent.UpdateItemUI();
            }
        }
    }
}