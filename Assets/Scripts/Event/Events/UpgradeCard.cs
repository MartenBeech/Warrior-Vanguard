using System.Collections.Generic;
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
                    List<Card> unupgradedCards = DeckManager.GetUnUpgradedCards();
                    for (int i = 0; i < eventManager.upgradeCardsOptions.Count; i++) {
                        if (unupgradedCards.Count == 0) {
                            eventManager.upgradeCardsOptions[i].gameObject.SetActive(false);
                            break;
                        }

                        Card randomCard = Rng.Entry(unupgradedCards);

                        eventManager.upgradeCardsOptions[i].SetStats(randomCard.stats);
                        eventManager.upgradeCardsOptions[i].SetHoverCardFromMap();
                        eventManager.upgradeCardsOptions[i].UpdateCardUI();
                        eventManager.cardIndexes.Add(randomCard);
                        unupgradedCards.RemoveAll(card => card.stats.title == randomCard.stats.title);
                    }

                    eventManager.SaveCardsEvent(eventManager.upgradeCardsOptions);
                }
            },
        };

        return newEvent;
    }
}