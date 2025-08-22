using System.Collections.Generic;
using UnityEngine;
public class RemoveCard {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "A guard blocks your path. He will only let you pass if you give him one of the following cards.";
                if (DeckManager.GetDeck().Count <= 0) {
                    eventManager.eventText.text += " However it seems like you don't have any cards to give him.. He will let you off with a warning.";
                    eventManager.FinishEvent();
                    return;
                }
                eventManager.cardOptionPanel.SetActive(true);
                foreach (var card in eventManager.cardOption) {
                    var button = card.GetComponent<UnityEngine.UI.Button>();
                    button.onClick.AddListener(() => eventManager.RemoveCard(card));
                }

                if (PlayerPrefs.HasKey(eventManager.eventCardsKey)) {
                    eventManager.LoadCardsEvent(eventManager.cardOption);
                } else {
                    List<Card> deck = DeckManager.LoadDeck();
                    for (int i = 0; i < eventManager.cardOption.Count; i++) {
                        if (deck.Count == 0) {
                            eventManager.cardOption[i].gameObject.SetActive(false);
                            break;
                        }

                        Card randomCard = Rng.Entry(deck);

                        eventManager.cardOption[i].SetStats(randomCard.stats);
                        eventManager.cardOption[i].SetHoverCardFromMap();
                        eventManager.cardOption[i].UpdateCardUI();
                        eventManager.cardIndexes.Add(randomCard);
                        deck.RemoveAll(card => card.stats.title == randomCard.stats.title);
                    }

                    eventManager.SaveCardsEvent(eventManager.cardOption);
                }
            },
        };

        return newEvent;
    }
}