using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using TMPro;
using System;
using System.Linq;

public class Collection : MonoBehaviour {
    public Transform subcategories;
    public Transform items;
    public GameObject subcategoryPrefab;
    public GameObject cardPrefab;
    private string selectedClass = "";
    private string selectedRace = "";

    public void SelectClass() {
        string title = EventSystem.current.currentSelectedGameObject.name;
        if (title == selectedClass) return;

        string targetPath;

        switch (title) {
            case "Spells":
                targetPath = Application.dataPath + $"/Scripts/Database/Spells";
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
        string title = EventSystem.current.currentSelectedGameObject.name;
        if (title == selectedRace) return;

        string targetPath;

        switch (selectedClass) {
            case "Spells":
                targetPath = Application.dataPath + $"/Scripts/Database/Spells/{title}";
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
                GameObject cardObj = Instantiate(cardPrefab, items);
                string fileTitle = Path.GetFileName(filePath).Split(".")[0];

                Type type = Type.GetType(fileTitle);
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
