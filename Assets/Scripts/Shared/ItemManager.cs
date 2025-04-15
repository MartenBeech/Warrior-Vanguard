using System;
using System.Collections.Generic;
using UnityEngine;

public static class ItemManager {
    static string itemKey = "playerItems";
    static string availableItemsKey = "availableItems";
    public static List<Item> items = new();
    private static List<Item> allItems = new();
    private static List<Item> availableItems = new();
    public static ItemsPanel ItemsPanel;

    private static void LoadAllItems() {
        Type[] itemTypes = new Type[] {
            typeof(Recycle),
            typeof(TurtleUp),
            typeof(TurtleAssembler),
            typeof(CrackedEgg),
            typeof(SmallHeart),
            typeof(BigHeart),
            typeof(WoodenSword),
            typeof(BigCoin),
        };

        foreach (var type in itemTypes) {
            Item item = CreateItemByTitle(type.Name);
            allItems.Add(item);
        }
    }

    public static void AddItem(Item item) {
        items = LoadItems();
        items.Add(item);
        SaveItems();
        ItemsPanel.UpdateUI();
    }

    public static Item GetRandomItem() {
        availableItems = LoadAvailableItems();

        int randomIndex = 0; // TODO: Replace this line with the below one after testing
        // int randomIndex = Rng.Range(0, availableItems.Count);
        Item randomItem = availableItems[randomIndex];
        availableItems.RemoveAt(randomIndex);
        SaveAvailableItems();
        return randomItem;
    }

    public static void InitAvailableItems() {
        LoadAllItems();
        availableItems = allItems;
        SaveAvailableItems();
    }

    private static void SaveItems() {
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

    private static void SaveAvailableItems() {
        List<string> itemTitles = new();
        foreach (var item in availableItems) {
            itemTitles.Add(item.title);
        }

        string itemData = string.Join(",", itemTitles);
        PlayerPrefs.SetString(availableItemsKey, itemData);
        PlayerPrefs.Save();
    }

    private static List<Item> LoadAvailableItems() {
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

    private static Item CreateItemByTitle(string title) {
        GameObject itemObj = new();

        if (title == "") {
            Item trashItem = itemObj.AddComponent<TrashItem>();
            return trashItem;
        }

        Type type = Type.GetType(title);
        Item itemComponent = (Item)itemObj.AddComponent(type);
        Item item = itemComponent.GetItem();
        return item;
    }
}