using System;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager {
    static string itemKey = "playerItems";
    static string availableItemsKey = "availableItems";
    public static List<Item> items = new();
    private static List<Item> allItems = new() {
            new CrackedEgg().GetItem(),
            new SmallHeart().GetItem(),
            new BigHeart().GetItem(),
            new WoodenSword().GetItem(),
            new BigCoin().GetItem(),
        };
    private static List<Item> availableItems = new();
    public static ItemsPanel ItemsPanel;

    public static void AddItem(Item item) {
        items = LoadItems();
        items.Add(item);
        SaveItems();
        ItemsPanel.UpdateUI();
    }

    public static Item GetRandomItem() {
        availableItems = LoadAvailableItems();

        int randomIndex = Rng.Range(0, availableItems.Count);
        Item randomItem = availableItems[randomIndex];
        availableItems.RemoveAt(randomIndex);
        SaveAvailableItems();
        return randomItem;
    }

    public static void InitAvailableItems() {
        availableItems = allItems;
        SaveAvailableItems();
    }

    public static void SaveItems() {
        List<string> itemTitles = new();
        foreach (var item in items) {
            itemTitles.Add(item.title);
        }

        string itemData = string.Join(",", itemTitles);
        PlayerPrefs.SetString(itemKey, itemData);
        PlayerPrefs.Save();
    }

    public static List<Item> LoadItems() {
        List<Item> tempItems = new();
        if (!PlayerPrefs.HasKey(itemKey)) return tempItems;

        string itemData = PlayerPrefs.GetString(itemKey);
        string[] itemTitles = itemData.Split(',');

        foreach (string title in itemTitles) {
            Item item = CreateItemByTitle(title);
            tempItems.Add(item);
        }

        return tempItems;
    }

    public static void SaveAvailableItems() {
        List<string> itemTitles = new();
        foreach (var item in availableItems) {
            itemTitles.Add(item.title);
        }

        string itemData = string.Join(",", itemTitles);
        PlayerPrefs.SetString(availableItemsKey, itemData);
        PlayerPrefs.Save();
    }

    public static List<Item> LoadAvailableItems() {
        List<Item> tempItems = new();
        if (!PlayerPrefs.HasKey(availableItemsKey)) return tempItems;

        string itemData = PlayerPrefs.GetString(availableItemsKey);
        string[] itemTitles = itemData.Split(',');

        foreach (string title in itemTitles) {
            Item item = CreateItemByTitle(title);
            tempItems.Add(item);
        }

        return tempItems;
    }

    // Match titles to item constructors
    private static Item CreateItemByTitle(string title) {
        if (title == "") return new TrashItem().GetItem();

        Type type = Type.GetType(title);
        object instance = Activator.CreateInstance(type);
        Item item = (Item)type.GetMethod("GetItem")?.Invoke(instance, null);
        return item;
    }
}