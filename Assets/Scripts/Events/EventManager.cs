using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
    public TMP_Text eventText;
    public List<Card> upgradeCardsOptions;
    public List<Card> removeCardsOptions;
    public GameObject upgradeCardPanel;
    public GameObject removeCardPanel;
    public DeckBuilder deckBuilder;
    private List<int> cardIndexes = new();

    void Start() {
        upgradeCardPanel.SetActive(false);
        removeCardPanel.SetActive(false);
        TriggerRandomEvent();
    }

    public void TriggerRandomEvent() {
        int randomEvent = Random.Range(0, 5);
        randomEvent = 4;

        switch (randomEvent) {
            case 0:
                GoldEvent();
                break;
            case 1:
                LoseGoldEvent();
                break;
            case 2:
                GainCardEvent();
                break;
            case 3:
                UpgradeCardEvent();
                break;
            case 4:
                RemoveCardEvent();
                break;
        }
    }

    void GoldEvent() {
        int goldGained = 50;
        GoldManager.AddGold(goldGained);
        eventText.text = $"You found treasure! Gained {goldGained} gold!";
    }

    void LoseGoldEvent() {
        int goldLost = 25;
        GoldManager.RemoveGold(goldLost);
        eventText.text = $"A thief stole {goldLost} gold from you!";
    }

    void GainCardEvent() {
        int randomIndex = Random.Range(0, CardDatabase.allCards.Count);
        Card card = new();
        card.SetStats(CardDatabase.allCards[randomIndex]);
        DeckManager.AddCard(card);
        eventText.text = $"You found a mysterious card and added it to your deck! {card.stats.title}";
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

    public void ReturnToMap() {
        SceneManager.LoadScene("Map");
    }
}
