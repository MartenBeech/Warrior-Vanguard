using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "You visited the friendly neighborhood smith.. He will upgrade one of your cards for free.";
                eventManager.option1Text.text = "Return";
                
                if (DeckManager.GetDeck().Count <= 0) {
                    eventManager.eventText.text += " However it seems like you don't have any cards to upgrade sorry";
                    eventManager.FinishEvent();
                    return;
                }
                eventManager.cardOptionPanel.SetActive(true);
                foreach (var card in eventManager.cardOption) {
                    var button = card.GetComponent<UnityEngine.UI.Button>();
                    button.onClick.AddListener(() => eventManager.UpgradeCard(card));
                }

                if (PlayerPrefs.HasKey(eventManager.eventCardsKey)) {
                    eventManager.LoadCardsEvent(eventManager.cardOption);
                } else {
                    List<Card> unupgradedCards = DeckManager.GetUnUpgradedCards();
                    for (int i = 0; i < eventManager.cardOption.Count; i++) {
                        if (unupgradedCards.Count == 0) {
                            eventManager.cardOption[i].gameObject.SetActive(false);
                            break;
                        }

                        Card randomCard = Rng.Entry(unupgradedCards);

                        eventManager.cardOption[i].SetStats(randomCard.stats);
                        eventManager.cardOption[i].SetHoverCardFromMap();
                        eventManager.cardOption[i].UpdateCardUI();
                        eventManager.cardIndexes.Add(randomCard);
                        unupgradedCards.RemoveAll(card => card.stats.title == randomCard.stats.title);
                    }

                    eventManager.SaveCardsEvent(eventManager.cardOption);
                }
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = "Apparently you don't want to be stronger.";
            },
        };

        return newEvent;
    }
}