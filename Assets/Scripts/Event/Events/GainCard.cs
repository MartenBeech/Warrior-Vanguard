using UnityEngine;

public class GainCard {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "You visited the friendly neighborhood papermaker.. He will give you one of his legendary cards for free.";
                eventManager.gainCardPanel.SetActive(true);
                if (PlayerPrefs.HasKey(eventManager.eventCardsKey)) {
                    eventManager.LoadCardsEvent(eventManager.gainCardsOptions);
                } else {
                    foreach (Card card in eventManager.gainCardsOptions) {
                        // Ensure we don't pick the same card twice
                        WarriorStats stats;
                        do {
                            stats = CardDatabase.GetRandomWarriorStats(CardRarity.Legendary);
                        } while (eventManager.gainCardsOptions.Exists(c => c.stats != null && c.stats.title == stats.title));
                        card.SetStats(stats);
                        card.SetHoverCardFromMap();
                        card.UpdateCardUI();
                    }

                    eventManager.SaveCardsEvent(eventManager.gainCardsOptions);
                }
            },
        };

        return newEvent;
    }
}