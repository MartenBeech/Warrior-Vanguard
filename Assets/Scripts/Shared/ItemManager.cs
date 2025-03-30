using System.Collections.Generic;
using UnityEngine;

public static class ItemManager {
    public static List<Item> items = new();
    private static List<Item> availableItems = new();
    public static ItemsPanel ItemsPanel;

    public static void AddItem(Item item) {
        items.Add(item);
        ItemsPanel.UpdateUI();
    }

    public static Item GetRandomItem() {
        if (availableItems.Count == 0) {
            return new TrashItem();
        }

        int randomIndex = Random.Range(0, availableItems.Count);
        Item randomItem = availableItems[randomIndex];
        availableItems.RemoveAt(randomIndex);
        return randomItem;
    }

    public static void InitAvailableItems() {
        availableItems = new List<Item> {
            new SmallHeart(),
            new BigHeart(),
        };
    }
}