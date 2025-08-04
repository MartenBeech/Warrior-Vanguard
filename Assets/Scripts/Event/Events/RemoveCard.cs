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
                    for (int i = 0; i < eventManager.removeCardsOptions.Count; i++) {
                        if (eventManager.cardIndexes.Count >= DeckManager.GetDeck().Count) {
                            eventManager.removeCardsOptions[i].gameObject.SetActive(false);
                            break;
                        }

                        // Get a random index from the deck, make sure we don't pick the same card twice
                        int randomIndex;
                        do {
                            randomIndex = Rng.Range(0, DeckManager.GetDeck().Count);
                        } while (eventManager.cardIndexes.Contains(randomIndex));
                        eventManager.removeCardsOptions[i].SetStats(DeckManager.GetCard(randomIndex).stats);
                        eventManager.removeCardsOptions[i].SetHoverCardFromMap();
                        eventManager.removeCardsOptions[i].UpdateCardUI();
                        eventManager.cardIndexes.Add(randomIndex);
                    }

                    eventManager.SaveCardsEvent(eventManager.removeCardsOptions);
                }
            },
        };

        return newEvent;
    }
}