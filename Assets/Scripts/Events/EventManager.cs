using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class EventManager : MonoBehaviour {
    string eventKey = "eventKey";
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
    events currentEvent;
    List<int> cardIndexes = new();
    int goldAmount = 0;
    int healthDifference = 0;
    Item item;

    void Start() {
        upgradeCardPanel.SetActive(false);
        removeCardPanel.SetActive(false);
        gainCardPanel.SetActive(false);
        acceptButton.SetActive(false);
        itemRewardPanel.SetActive(false);
        TriggerRandomEvent();
    }

    public void TriggerRandomEvent() {
        int randomIndex = 0;
        if (PlayerPrefs.HasKey(eventKey)) {
            randomIndex = PlayerPrefs.GetInt(eventKey);
        } else {
            randomIndex = Random.Range(0, System.Enum.GetValues(typeof(events)).Length);
        }
        currentEvent = (events)randomIndex;

        PlayerPrefs.SetInt(eventKey, randomIndex);
        PlayerPrefs.Save();

        // currentEvent = events.GainItemEvent; // Hardcoded for testing
        switch (currentEvent) {
            case events.GainGoldEvent:
                GainGoldEvent();
                break;
            case events.LoseGoldEvent:
                LoseGoldEvent();
                break;
            case events.GainCardEvent:
                GainCardEvent();
                break;
            case events.UpgradeCardEvent:
                UpgradeCardEvent();
                break;
            case events.RemoveCardEvent:
                RemoveCardEvent();
                break;
            case events.LoseHealthEvent:
                LoseHealthEvent();
                break;
            case events.GainMaxHealthEvent:
                GainMaxHealthEvent();
                break;
            case events.GainItemEvent:
                GainItemEvent();
                break;
        }
    }

    void GainGoldEvent() {
        goldAmount = 50;
        eventText.text = $"You found treasure! Inside is {goldAmount} gold! Do you take it?";
        acceptButton.SetActive(true);
    }

    void LoseGoldEvent() {
        int goldAmount = 25;
        GoldManager.RemoveGold(goldAmount);
        eventText.text = $"A thief stole {goldAmount} gold from you! You cry for a bit but then realise your time is better spent moving on.";
        FinishEvent();
    }

    void GainCardEvent() {
        eventText.text = "You visited the friendly neighborhood papermaker.. He will give you one of his legendary cards for free.";
        gainCardPanel.SetActive(true);

        foreach (Card card in gainCardsOptions) {
            WarriorStats stats = CardDatabase.GetRandomWarriorStats(CardRarity.Legendary);
            card.SetStats(stats);
            card.UpdateCardUi();
        }
    }

    void UpgradeCardEvent() {
        eventText.text = "You visited the friendly neighborhood smith.. He will upgrade one of your cards for free.";
        if (DeckManager.GetDeck().Count <= 0) {
            eventText.text += " However it seems like you don't have any cards to upgrade sorry";
            return;
        }

        upgradeCardPanel.SetActive(true);

        foreach (Card card in upgradeCardsOptions) {
            int randomIndex = Random.Range(0, DeckManager.GetUnUpgradedCards().Count);
            card.SetStats(DeckManager.GetCard(randomIndex).stats);
            card.UpdateCardUi();
            cardIndexes.Add(randomIndex);
        }
    }

    void LoseHealthEvent() {
        healthDifference = 5;
        summonerManager.LoseHealth(healthDifference);
        eventText.text = $"An unfriendly skeleton walks up to you and hit you in the face. You leave with {healthDifference} less health and a lot of questions.";
        FinishEvent();
    }

    void GainMaxHealthEvent() {
        healthDifference = 5;
        summonerManager.GainMaxHealth(healthDifference);
        eventText.text = $"A stranger walks up to you and gives you a potion. Without hesitation, you drink it and gains {healthDifference} max health. You are happy about your life choices.";
        FinishEvent();
    }

    void RemoveCardEvent() {
        eventText.text = "You visited the friendly neighborhood barber.. He will cut one of your cards for free.";
        if (DeckManager.GetDeck().Count <= 0) {
            eventText.text += " However it seems like you don't have any cards to cut sorry. You should probably get some cards..";
            return;
        }

        removeCardPanel.SetActive(true);

        foreach (Card card in removeCardsOptions) {
            int randomIndex = Random.Range(0, DeckManager.GetDeck().Count);
            card.SetStats(DeckManager.GetCard(randomIndex).stats);
            card.UpdateCardUi();
            cardIndexes.Add(randomIndex);
        }
    }

    void GainItemEvent() {
        item = ItemManager.GetRandomItem();

        if (item.title == null) {
            eventText.text = "You find a hole where an item used to be. However, it seems like you already have all the items in the world.";
            return;
        }

        itemRewardPanel.SetActive(true);
        itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{item.title}");
        itemTitle.GetComponent<TMP_Text>().text = item.displayTitle;
        itemDescription.GetComponent<TMP_Text>().text = item.description;

        eventText.text = $"You find a mysterious item on the ground called {item.displayTitle}. Do you want to pick it up?";
        acceptButton.SetActive(true);
    }

    public void AcceptEvent() {
        switch (currentEvent) {
            case events.GainGoldEvent:
                GoldManager.AddGold(goldAmount);
                eventText.text = $"Congratulations! You are now a bit richer than before.";
                acceptButton.SetActive(false);
                break;
            case events.GainItemEvent:
                item.UseImmediately();
                ItemManager.AddItem(item);
                eventText.text = $"You picked up {item.displayTitle}!";
                acceptButton.SetActive(false);
                itemRewardPanel.SetActive(false);
                break;
            default:
                eventText.text = "This unfortunately doesn't seem to do anything.";
                break;
        }
        FinishEvent();
    }

    public void UpgradeCard(int index) {
        upgradeCardPanel.SetActive(false);
        Card card = DeckManager.GetCard(cardIndexes[index]);
        card.stats.level = 1;
        eventText.text = $"You upgraded {card.stats.displayTitle}!";
        DeckManager.SaveDeck();
        FinishEvent();
    }

    public void RemoveCard(int index) {
        removeCardPanel.SetActive(false);
        Card card = DeckManager.GetCard(cardIndexes[index]);
        deckBuilder.RemoveCardFromDeck(cardIndexes[index]);
        eventText.text = $"You removed {card.stats.displayTitle} from your deck! We will not be seeing much more of them.";
        FinishEvent();
    }

    public void GainCard(Card card) {
        gainCardPanel.SetActive(false);
        deckBuilder.AddCardToDeck(card);
        eventText.text = $"You added {card.stats.displayTitle} to your deck!";
        FinishEvent();
    }

    public void FinishEvent() {
        PlayerPrefs.DeleteKey(eventKey);
        TileCompleter.MarkTileAsCompleted();
    }

    public void ReturnToMap() {
        FinishEvent();
        SceneManager.LoadScene("Map");
    }
}
