using System.Collections.Generic;
using UnityEngine;

public class GainLegendary {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "You visited the friendly neighborhood papermaker.. He will give you one of his legendary cards for free.";
                eventManager.option1Text.text = "Return";
                
                eventManager.cardOptionPanel.SetActive(true);
                foreach (var card in eventManager.cardOption) {
                    var button = card.GetComponent<UnityEngine.UI.Button>();
                    button.onClick.AddListener(() => eventManager.GainCard(card));
                }

                if (PlayerPrefs.HasKey(eventManager.eventCardsKey)) {
                    eventManager.LoadCardsEvent(eventManager.cardOption);
                } else {
                    List<WarriorStats> legendaryCards = CardDatabase.allCards.FindAll(card => card.rarity == CardRarity.Legendary);
                    foreach (Card card in eventManager.cardOption) {

                        WarriorStats randomStats = Rng.Entry(legendaryCards);

                        card.SetStats(randomStats);
                        card.SetHoverCardFromMap();
                        card.UpdateCardUI();
                        legendaryCards.RemoveAll(legendaryCard => legendaryCard.title == randomStats.title);
                    }

                    eventManager.SaveCardsEvent(eventManager.cardOption);
                }
            },
            
            OnClickOption1 = () => {
                eventManager.eventText.text = "Do you have any idea how much these are worth?!";
            },
        };

        return newEvent;
    }
}