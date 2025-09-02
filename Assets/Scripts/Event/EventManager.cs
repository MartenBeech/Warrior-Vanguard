using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;

public class EventManager : MonoBehaviour {
    public string eventKey = "eventKey";
    public string eventGainItemKey = "eventGainItem";
    public string eventCardsKey = "eventCards";
    public TMP_Text eventText;
    public List<Card> cardOption;
    public GameObject cardOptionPanel;
    public GameObject option1Button;
    public GameObject option2Button;
    public GameObject option3Button;
    public TMP_Text option1Text;
    public TMP_Text option2Text;
    public TMP_Text option3Text;
    public GameObject returnButton;
    public GameObject itemRewardPanel;
    public GameObject itemImage;
    public GameObject itemTitle;
    public GameObject itemDescription;
    public DeckBuilder deckBuilder;
    public SummonerManager summonerManager;
    enum events {
        GainGoldEvent,
        GainItemEvent,
        GainLegendaryEvent,
        GainMaxHealthEvent,
        GamblerEvent,
        UpgradeCardEvent,
        RemoveCardEvent,
    }
    Event currentEvent;
    public List<Card> cardIndexes = new();
    public Item item;
    public GameObject cardItemRewardPanel;
    public Card cardItemRewardCard;
    public TMP_Text cardItemRewardAmountText;
    public Item cardItemRewardItem;
    public GameObject cardItemRewardItemImage;
    public GameObject cardItemRewardItemTitle;
    public GameObject cardItemRewardItemDescription;
    public int eventPanels = 0;

    void Start() {
        cardOptionPanel.SetActive(false);
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        option3Button.SetActive(false);
        returnButton.SetActive(false);
        itemRewardPanel.SetActive(false);
        cardItemRewardPanel.SetActive(false);
        TriggerRandomEvent();
    }

    public void TriggerRandomEvent() {
        string fileTitle = "";
        if (PlayerPrefs.HasKey(eventKey)) {
            fileTitle = PlayerPrefs.GetString(eventKey);
        } else {
            string targetPath = Application.dataPath + $"/Scripts/Event/Events";

            if (Directory.Exists(targetPath)) {
                List<string> filePaths = Directory.GetFiles(targetPath, "*.cs").ToList();
                string filePath = Rng.Entry(filePaths);

                fileTitle = Path.GetFileName(filePath).Split(".")[0];
                fileTitle = "UpgradeCard"; // TODO: Hardcoded for testing purposes

                PlayerPrefs.SetString(eventKey, fileTitle);
                PlayerPrefs.Save();
            }
        }

        Type type = Type.GetType(fileTitle);
        object instance = Activator.CreateInstance(type);
        Event mapEvent = (Event)type.GetMethod("GetEvent")?.Invoke(instance, new object[] { this });
        currentEvent = mapEvent;

        currentEvent.OnSetup();

        if (currentEvent.OnClickOption1 != null) option1Button.SetActive(true);
        if (currentEvent.OnClickOption2 != null) option2Button.SetActive(true);
        if (currentEvent.OnClickOption3 != null) option3Button.SetActive(true);
        option1Button.GetComponent<UnityEngine.UI.Button>().interactable = currentEvent.enableOption1Button;
        option2Button.GetComponent<UnityEngine.UI.Button>().interactable = currentEvent.enableOption2Button;
        option3Button.GetComponent<UnityEngine.UI.Button>().interactable = currentEvent.enableOption3Button;
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

    public void ClickOption1() {
        currentEvent.OnClickOption1();
        if (eventPanels <= 0) FinishEvent();
    }

    public void ClickOption2() {
        currentEvent.OnClickOption2();
        if (eventPanels <= 0) FinishEvent();
    }

    public void ClickOption3() {
        currentEvent.OnClickOption3();
        if (eventPanels <= 0) FinishEvent();
    }

    public void UpgradeCard(Card card) {
        deckBuilder.UpgradeCardInDeck(card);
        eventText.text = $"You upgraded {card.stats.displayTitle}!";
        card.SetHoverCardFromMap();
        card.HideCard();
        DeckManager.SaveDeck();
        FinishEvent();
    }

    public void RemoveCard(Card card) {
        deckBuilder.RemoveCardFromDeck(card);
        eventText.text = $"You removed {card.stats.displayTitle} from your deck!";
        card.SetHoverCardFromMap();
        card.HideCard();
        FinishEvent();
    }

    public void GainCard(Card card) {
        deckBuilder.AddCardToDeck(card);
        eventText.text = $"You added {card.stats.displayTitle} to your deck!";
        card.SetHoverCardFromMap();
        card.HideCard();
        FinishEvent();
    }

    public void CopyCard(Card card) {
        deckBuilder.AddCardToDeck(card);
        eventText.text = $"'Great choice' you mumble. 'Great choice' the mirror mumbles back.";
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
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        option3Button.SetActive(false);
        cardOptionPanel.SetActive(false);
        StartCoroutine(FadeInButton(returnButton, 1f));
    }

    //Creates a smooth fade-in effect for the button
    private System.Collections.IEnumerator FadeInButton(GameObject button, float duration) {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        if (canvasGroup == null) {
            canvasGroup = button.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0f;
        button.SetActive(true);

        float elapsed = 0f;
        while (elapsed < duration) {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    public void ReturnToMap() {
        SceneManager.LoadScene("Map");
    }
}

