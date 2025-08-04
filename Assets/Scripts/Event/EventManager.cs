using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;

public class EventManager : MonoBehaviour {
    public string eventKey = "eventKey";
    public string eventGainItemKey = "eventGainItem";
    public string eventCardsKey = "eventCards";
    public TMP_Text eventText;
    public List<Card> upgradeCardsOptions;
    public List<Card> removeCardsOptions;
    public List<Card> gainCardsOptions;
    public GameObject upgradeCardPanel;
    public GameObject removeCardPanel;
    public GameObject gainCardPanel;
    public GameObject acceptButton;
    public GameObject itemRewardPanel;
    public GameObject itemImage;
    public GameObject itemTitle;
    public GameObject itemDescription;
    public DeckBuilder deckBuilder;
    public SummonerManager summonerManager;
    enum events {
        GainGoldEvent,
        LoseGoldEvent,
        GainCardEvent,
        UpgradeCardEvent,
        RemoveCardEvent,
        LoseHealthEvent,
        GainMaxHealthEvent,
        GainItemEvent,
    }
    Event currentEvent;
    public List<Card> cardIndexes = new();
    public Item item;

    void Start() {
        upgradeCardPanel.SetActive(false);
        removeCardPanel.SetActive(false);
        gainCardPanel.SetActive(false);
        acceptButton.SetActive(false);
        itemRewardPanel.SetActive(false);
        TriggerRandomEvent();
    }

    public void TriggerRandomEvent() {
        string fileTitle;
        if (PlayerPrefs.HasKey(eventKey)) {
            fileTitle = PlayerPrefs.GetString(eventKey);
        } else {
            string targetPath = Application.dataPath + $"/Scripts/Event/Events";

            if (Directory.Exists(targetPath)) {
                List<string> filePaths = Directory.GetFiles(targetPath, "*.cs").ToList();
                string filePath = Rng.Entry(filePaths);

                fileTitle = Path.GetFileName(filePath).Split(".")[0];

                fileTitle = "UpgradeCard"; // TODO: Hardcoded for testing purposes

                Type type = Type.GetType(fileTitle);
                object instance = Activator.CreateInstance(type);
                Event mapEvent = (Event)type.GetMethod("GetEvent")?.Invoke(instance, new object[] { this });

                currentEvent = mapEvent;

                PlayerPrefs.SetString(eventKey, fileTitle);
                PlayerPrefs.Save();
            }
        }

        currentEvent.OnSetup();

        if (currentEvent.OnAccept != null) {
            acceptButton.SetActive(true);
        } else {
            FinishEvent();
        }
    }

    public void SaveCardsEvent(List<Card> cards) {
        List<string> cardTitlesAndLevels = new();
        foreach (Card card in cards) {
            cardTitlesAndLevels.Add($"{card.stats.title}_{card.stats.level}");
        }

        string cardData = string.Join(",", cardTitlesAndLevels);
        PlayerPrefs.SetString(eventCardsKey, cardData);
        PlayerPrefs.Save();
    }

    public void LoadCardsEvent(List<Card> cards) {
        string cardData = PlayerPrefs.GetString(eventCardsKey);
        string[] cardTitlesAndLevels = cardData.Split(',');

        for (int i = 0; i < cards.Count; i++) {
            if (i >= cardTitlesAndLevels.Length) {
                // This happens if cards have already been bought, and then reloading the shop.
                cards[i].gameObject.SetActive(false);
            } else {
                WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(cardTitlesAndLevels[i]);
                cards[i].SetStats(stats);
                cards[i].SetHoverCardFromMap();
                cards[i].UpdateCardUI();
            }
        }
    }

    public void AcceptEvent() {
        currentEvent.OnAccept();
        acceptButton.SetActive(false);
        FinishEvent();
    }

    public void UpgradeCard(Card card) {
        upgradeCardPanel.SetActive(false);
        deckBuilder.UpgradeCardInDeck(card);
        eventText.text = $"You upgraded {card.stats.displayTitle}!";
        card.SetHoverCardFromMap();
        card.HideCard();
        DeckManager.SaveDeck();
        FinishEvent();
    }

    public void RemoveCard(Card card) {
        removeCardPanel.SetActive(false);
        deckBuilder.RemoveCardFromDeck(card);
        eventText.text = $"You removed {card.stats.displayTitle} from your deck! We will not be seeing much more of them.";
        card.SetHoverCardFromMap();
        card.HideCard();
        FinishEvent();
    }

    public void GainCard(Card card) {
        gainCardPanel.SetActive(false);
        deckBuilder.AddCardToDeck(card);
        eventText.text = $"You added {card.stats.displayTitle} to your deck!";
        card.SetHoverCardFromMap();
        card.HideCard();
        FinishEvent();
    }

    public void FinishEvent() {
        PlayerPrefs.DeleteKey(eventKey);
        PlayerPrefs.DeleteKey(eventGainItemKey);
        PlayerPrefs.DeleteKey(eventCardsKey);
        TileCompleter.MarkTileAsCompleted();
        cardIndexes.Clear();
    }

    public void ReturnToMap() {
        FinishEvent();
        SceneManager.LoadScene("Map");
    }
}
