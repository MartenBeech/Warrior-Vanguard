using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class Collection : MonoBehaviour {
    public Transform subcategories;
    public Transform items;
    public GameObject subcategoryPrefab;
    public GameObject cardPrefab;
    public GameObject itemPrefab;
    private string selectedClass = "";
    private string selectedRace = "";
    private Button lastClickedClassButton;
    private Button lastClickedRaceButton;

    public void SelectClass() {
        Button buttonClicked = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        string title = buttonClicked.name;
        if (title == selectedClass) return;

        if (lastClickedClassButton != null) {
            lastClickedClassButton.GetComponent<Image>().color = ColorPalette.AddTransparency(lastClickedClassButton.GetComponent<Image>().color, 100);
        }
        buttonClicked.GetComponent<Image>().color = ColorPalette.AddTransparency(buttonClicked.GetComponent<Image>().color, 65);
        lastClickedClassButton = buttonClicked;

        string targetPath;

        switch (title) {
            case "Spells":
                targetPath = Application.dataPath + $"/Scripts/Database/Spells";
                break;

            case "Items":
                targetPath = Application.dataPath + $"/Scripts/Database/Items";
                break;

            default:
                targetPath = Application.dataPath + $"/Scripts/Database/Warriors/{title}";
                break;
        }

        if (Directory.Exists(targetPath)) {

            for (int i = subcategories.childCount - 1; i >= 0; i--) {
                Destroy(subcategories.GetChild(i).gameObject);
            }
            for (int i = items.childCount - 1; i >= 0; i--) {
                Destroy(items.GetChild(i).gameObject);
            }

            selectedClass = title;
            selectedRace = "";

            string[] subfolders = Directory.GetDirectories(targetPath);
            foreach (string folderPath in subfolders) {
                GameObject folderObj = Instantiate(subcategoryPrefab, subcategories);
                string folderTitle = Path.GetFileName(folderPath);
                int nFilesInFolder = Directory.GetFiles(folderPath, "*.cs").Count();
                folderObj.GetComponentInChildren<TextMeshProUGUI>().text = $"{folderTitle} ({nFilesInFolder})";
                folderObj.name = folderTitle;

                Button button = folderObj.GetComponent<Button>();
                button.onClick.AddListener(SelectRace);
            }
        }

        return;
    }

    public void SelectRace() {
        Button buttonClicked = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        string title = buttonClicked.name;
        if (title == selectedRace) return;

        if (lastClickedRaceButton != null) {
            lastClickedRaceButton.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
        }
        buttonClicked.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.tealMedium);
        lastClickedRaceButton = buttonClicked;

        string targetPath;

        switch (selectedClass) {
            case "Spells":
                targetPath = Application.dataPath + $"/Scripts/Database/Spells/{title}";
                break;

            case "Items":
                targetPath = Application.dataPath + $"/Scripts/Database/Items/{title}";
                break;

            default:
                targetPath = Application.dataPath + $"/Scripts/Database/Warriors/{selectedClass}/{title}";
                break;
        }

        if (Directory.Exists(targetPath)) {

            for (int i = items.childCount - 1; i >= 0; i--) {
                Destroy(items.GetChild(i).gameObject);
            }

            selectedRace = title;

            string[] files = Directory.GetFiles(targetPath, "*.cs");
            foreach (string filePath in files) {
                string fileTitle = Path.GetFileName(filePath).Split(".")[0];
                Type type = Type.GetType(fileTitle);

                if (selectedClass == "Items") {
                    GameObject itemObj = Instantiate(itemPrefab, items);

                    GameObject tempItemObj = new();
                    Item itemComponent = (Item)tempItemObj.AddComponent(type);
                    Item item = itemComponent.GetItem();
                    item.displayTitle = Regex.Replace(item.title, "(?<!^)([A-Z])", " $1");

                    itemObj.GetComponent<Item>().SetItem(item);
                    itemObj.transform.localScale = new Vector2(2, 2);
                } else {
                    GameObject cardObj = Instantiate(cardPrefab, items);

                    object instance = Activator.CreateInstance(type);
                    WarriorStats stats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);

                    cardObj.transform.localScale = new Vector2(2, 2);
                    cardObj.GetComponent<DragDrop>().enabled = false;
                    cardObj.GetComponent<ObjectAnimation>().enabled = false;

                    Card card = cardObj.GetComponent<Card>();
                    card.SetStats(stats);
                    card.UpdateCardUI();
                    card.SetHoverCardFromCollection();
                }
            }
        }
    }
}
