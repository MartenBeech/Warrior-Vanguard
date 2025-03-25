using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
    public TMP_Text eventText;
    public List<Card> upgradeCardsOptions;
    public List<Card> removeCardsOptions;
    public List<Card> gainCardsOptions;
    public GameObject upgradeCardPanel;
    public GameObject removeCardPanel;
    public GameObject gainCardPanel;
    public GameObject acceptButton;
    public DeckBuilder deckBuilder;
    enum eventIndex {
        GainGoldEvent,
        LoseGoldEvent,
        GainCardEvent,
        UpgradeCardEvent,
        RemoveCardEvent
    }
    eventIndex currentEvent;
    List<int> cardIndexes = new();
    int goldAmount = 0;

    void Start() {
        upgradeCardPanel.SetActive(false);
        removeCardPanel.SetActive(false);
        gainCardPanel.SetActive(false);
        acceptButton.SetActive(false);
        TriggerRandomEvent();
    }

    public void TriggerRandomEvent() {
        int randomIndex = Random.Range(0, 5);
        currentEvent = (eventIndex)randomIndex;

        currentEvent = eventIndex.GainGoldEvent;
        switch (currentEvent) {
            case eventIndex.GainGoldEvent:
                GainGoldEvent();
                break;
            case eventIndex.LoseGoldEvent:
                LoseGoldEvent();
                break;
            case eventIndex.GainCardEvent:
                GainCardEvent();
                break;
            case eventIndex.UpgradeCardEvent:
                UpgradeCardEvent();
                break;
            case eventIndex.RemoveCardEvent:
                RemoveCardEvent();
                break;
        }
    }

    void GainGoldEvent() {
        goldAmount = 50;
        eventText.text = $"You found treasure! Inside is {goldAmount} gold! Do you take it?";
        acceptButton.SetActive(true);
    }

    void LoseGoldEvent() {
        int goldLost = 25;
        GoldManager.RemoveGold(goldLost);
        eventText.text = $"A thief stole {goldLost} gold from you!";
    }

    void GainCardEvent() {
        eventText.text = "You visited the friendly neighborhood papermaker.. He will give you one of his legendary cards for free";
        gainCardPanel.SetActive(true);

        foreach (Card card in gainCardsOptions) {
            WarriorStats stats = CardDatabase.GetRandomWarriorStats(CardRarity.Legendary);
            Debug.Log(stats.title);
            card.SetStats(stats);
            card.UpdateCardUi();
        }
    }

    void UpgradeCardEvent() {
        eventText.text = "You visited the friendly neighborhood smith.. He will upgrade one of your cards for free";
        if (DeckManager.GetDeck().Count <= 0) {
            eventText.text += ", but you have any cards to upgrade sorry";
            return;
        }
        ;

        upgradeCardPanel.SetActive(true);

        foreach (Card card in upgradeCardsOptions) {
            int randomIndex = Random.Range(0, DeckManager.GetDeck().Count);
            card.SetStats(DeckManager.GetCard(randomIndex).stats);
            card.UpdateCardUi();
            cardIndexes.Add(randomIndex);
        }
    }

    void RemoveCardEvent() {
        eventText.text = "You visited the friendly neighborhood barber.. He will cut one of your cards for free";
        if (DeckManager.GetDeck().Count <= 0) {
            eventText.text += ", but you have any cards to cut sorry";
            return;
        }
        ;

        removeCardPanel.SetActive(true);

        foreach (Card card in removeCardsOptions) {
            int randomIndex = Random.Range(0, DeckManager.GetDeck().Count);
            card.SetStats(DeckManager.GetCard(randomIndex).stats);
            card.UpdateCardUi();
            cardIndexes.Add(randomIndex);
        }
    }

    public void AcceptEvent() {
        switch (currentEvent) {
            case eventIndex.GainGoldEvent:
                GoldManager.AddGold(goldAmount);
                eventText.text = $"Congratulations! You are now a bit richer than before";
                acceptButton.SetActive(false);
                break;
            case eventIndex.LoseGoldEvent:
                LoseGoldEvent();
                break;
            case eventIndex.GainCardEvent:
                GainCardEvent();
                break;
            case eventIndex.UpgradeCardEvent:
                UpgradeCardEvent();
                break;
            case eventIndex.RemoveCardEvent:
                RemoveCardEvent();
                break;
        }
    }

    public void UpgradeCard(int index) {
        upgradeCardPanel.SetActive(false);
        Card card = DeckManager.GetCard(cardIndexes[index]);
        card.stats.level += 1;
        card.stats.title += "+";
        eventText.text = $"You upgraded {card.stats.title}!";
    }

    public void RemoveCard(int index) {
        removeCardPanel.SetActive(false);
        Card card = DeckManager.GetCard(cardIndexes[index]);
        deckBuilder.RemoveCardFromDeck(cardIndexes[index]);
        eventText.text = $"You removed {card.stats.title} from your deck! We will not be seeing much more of them";
    }

    public void GainCard(Card card) {
        gainCardPanel.SetActive(false);
        deckBuilder.AddCardToDeck(card);
        eventText.text = $"You added {card.stats.title} to your deck!";
    }

    public void ReturnToMap() {
        SceneManager.LoadScene("Map");
    }
}
