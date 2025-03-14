using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EventManager : MonoBehaviour {
    public TMP_Text eventText;

    void Start() {
        TriggerRandomEvent();
    }

    public void TriggerRandomEvent() {
        int randomEvent = Random.Range(0, 4); // Random number between 0-3

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
        if (DeckManager.GetDeck().Count <= 0) {
            eventText.text = $"A magical force strengthened one of your cards! You have no cards to upgrade sorry";
            return;
        }
        ;

        int randomIndex = Random.Range(0, DeckManager.GetDeck().Count);
        Card card = DeckManager.GetCard(randomIndex);
        card.stats.level += 1;
        card.stats.title += "+";
        eventText.text = $"A magical force strengthened one of your cards! {card.stats.title}";
    }

    public void ReturnToMap() {
        SceneManager.LoadScene("Map");
    }
}
