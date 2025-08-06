using System.Collections.Generic;
using UnityEngine;

public class GainLegendary {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "You visited the friendly neighborhood papermaker.. He will give you one of his legendary cards for free.";
                eventManager.gainCardPanel.SetActive(true);
                if (PlayerPrefs.HasKey(eventManager.eventCardsKey)) {
                    eventManager.LoadCardsEvent(eventManager.gainCardsOptions);
                } else {
                    List<WarriorStats> legendaryCards = CardDatabase.allCards.FindAll(card => card.rarity == CardRarity.Legendary);
                    foreach (Card card in eventManager.gainCardsOptions) {

                        WarriorStats randomStats = Rng.Entry(legendaryCards);

                        card.SetStats(randomStats);
                        card.SetHoverCardFromMap();
                        card.UpdateCardUI();
                        legendaryCards.RemoveAll(legendaryCard => legendaryCard.title == randomStats.title);
                    }

                    eventManager.SaveCardsEvent(eventManager.gainCardsOptions);
                }
            },
        };

        return newEvent;
    }
}