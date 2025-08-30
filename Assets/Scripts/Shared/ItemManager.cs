using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class ItemManager {
    static string itemKey = "playerItems";
    static string availableItemsKey = "availableItems";
    public static List<Item> items = new();
    public static Item enemyItem = null;
    public static List<Item> availableItems = new();
    public static ItemsPanel ItemsPanel;

    private static List<Item> LoadAllItems() {
        List<Item> allItems = new();
        Type[] itemTypes = new Type[] {
            typeof(Underdog),
            typeof(UnfinishedPuzzle),
            typeof(MicDrop),
            typeof(Tactics),
            typeof(ExplosiveDevice),
            typeof(VampireRing),
            typeof(HumanRing),
            typeof(HolyLight),
            typeof(ThunderCloud),
            typeof(WarmWelcome),
            typeof(Recycle),
            typeof(TurtleUp),
            typeof(TurtleAssembler),
            typeof(CrackedEgg),
            typeof(SmallHeart),
            typeof(BigHeart),
            typeof(WoodenSword),
            typeof(MoneyBag),
        };

        foreach (var type in itemTypes) {
            Item item = GetItemByTitle(type.Name);
            allItems.Add(item);
        }

        return allItems;
    }

    public static void AddItem(Item item) {
        items = LoadItems();
        items.Add(item);
        RemoveItemFromAvailable(item);
        SaveItems();
        ItemsPanel.UpdateUI();
    }

    public static void RemoveItem(Item item) {
        items = LoadItems();
        items.RemoveAll(i => i.title == item.title);
        SaveItems();
        ItemsPanel.UpdateUI();
    }

    public static Item GetRandomItem() {
        availableItems = LoadAvailableItems();

        // Item randomItem = Rng.Entry(availableItems);
        Item randomItem = availableItems[0]; //TODO: Hardcoded for testing purposes
        return randomItem;
    }

    public static void RemoveItemFromAvailable(Item item) {
        availableItems.RemoveAll(i => i.title == item.title);
        SaveAvailableItems();
    }

    public static void InitAvailableItems() {
        availableItems = LoadAllItems();
        SaveAvailableItems();
    }

    private static void SaveItems() {
        List<string> itemTitles = new();
        foreach (Item item in items) {
            if (item.title == null) continue;
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
        if (itemData == "") return tempItems;

        string[] itemTitles = itemData.Split(',');

        foreach (string title in itemTitles) {
            Item item = GetItemByTitle(title);
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

    public static List<Item> LoadAvailableItems() {
        List<Item> tempItems = new();
        if (!PlayerPrefs.HasKey(availableItemsKey)) return availableItems;

        string itemData = PlayerPrefs.GetString(availableItemsKey);
        string[] itemTitles = itemData.Split(',');

        foreach (string title in itemTitles) {
            Item item = GetItemByTitle(title);
            tempItems.Add(item);
        }

        availableItems = tempItems;
        return tempItems;
    }

    public static Item GetItemByTitle(string title) {
        GameObject itemObj = new();

        if (title == "") {
            Item trashItem = itemObj.AddComponent<TrashItem>();
            return trashItem;
        }

        Type type = Type.GetType(title);
        Item itemComponent = (Item)itemObj.AddComponent(type);
        Item item = itemComponent.GetItem();
        item.displayTitle = Regex.Replace(item.title, "(?<!^)([A-Z])", " $1");

        UnityEngine.Object.Destroy(itemObj);
        return item;
    }
}