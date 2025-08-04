using UnityEngine;

public class UpgradeCard {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "You visited the friendly neighborhood smith.. He will upgrade one of your cards for free.";
                if (DeckManager.GetDeck().Count <= 0) {
                    eventManager.eventText.text += " However it seems like you don't have any cards to upgrade sorry";
                    return;
                }
                eventManager.upgradeCardPanel.SetActive(true);

                if (PlayerPrefs.HasKey(eventManager.eventCardsKey)) {
                    eventManager.LoadCardsEvent(eventManager.upgradeCardsOptions);
                } else {
                    for (int i = 0; i < eventManager.upgradeCardsOptions.Count; i++) {
                        if (eventManager.cardIndexes.Count >= DeckManager.GetUnUpgradedCards().Count) {
                            eventManager.upgradeCardsOptions[i].gameObject.SetActive(false);
                            break;
                        }

                        // Get a random index from the un-upgraded cards, make sure we don't pick the same card twice
                        int randomIndex;
                        do {
                            randomIndex = Rng.Range(0, DeckManager.GetUnUpgradedCards().Count);
                        } while (eventManager.cardIndexes.Contains(randomIndex));
                        eventManager.upgradeCardsOptions[i].SetStats(DeckManager.GetCard(DeckManager.GetUnUpgradedCards()[randomIndex]).stats);
                        eventManager.upgradeCardsOptions[i].SetHoverCardFromMap();
                        eventManager.upgradeCardsOptions[i].UpdateCardUI();
                        eventManager.cardIndexes.Add(DeckManager.GetUnUpgradedCards()[randomIndex]);
                    }

                    eventManager.SaveCardsEvent(eventManager.upgradeCardsOptions);
                }
            },
        };

        return newEvent;
    }
}