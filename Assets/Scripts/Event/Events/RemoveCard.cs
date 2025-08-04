using System.Collections.Generic;
using UnityEngine;
public class RemoveCard {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "You visited the friendly neighborhood barber.. He will cut one of your cards for free.";
                if (DeckManager.GetDeck().Count <= 0) {
                    eventManager.eventText.text += " However it seems like you don't have any cards to cut sorry. You should probably get some cards..";
                    return;
                }
                eventManager.removeCardPanel.SetActive(true);

                if (PlayerPrefs.HasKey(eventManager.eventCardsKey)) {
                    eventManager.LoadCardsEvent(eventManager.removeCardsOptions);
                } else {
                    List<Card> deck = DeckManager.LoadDeck();
                    for (int i = 0; i < eventManager.removeCardsOptions.Count; i++) {
                        if (deck.Count == 0) {
                            eventManager.removeCardsOptions[i].gameObject.SetActive(false);
                            break;
                        }

                        Card randomCard = Rng.Entry(deck);

                        eventManager.removeCardsOptions[i].SetStats(randomCard.stats);
                        eventManager.removeCardsOptions[i].SetHoverCardFromMap();
                        eventManager.removeCardsOptions[i].UpdateCardUI();
                        eventManager.cardIndexes.Add(randomCard);
                        deck.RemoveAll(card => card.stats.title == randomCard.stats.title);
                    }

                    eventManager.SaveCardsEvent(eventManager.removeCardsOptions);
                }
            },
        };

        return newEvent;
    }
}