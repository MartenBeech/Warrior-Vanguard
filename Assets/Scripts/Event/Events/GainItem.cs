using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GainItem {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                if (PlayerPrefs.HasKey(eventManager.eventGainItemKey)) {
                    eventManager.item = ItemManager.GetItemByTitle(PlayerPrefs.GetString(eventManager.eventGainItemKey));
                } else {
                    eventManager.item = ItemManager.GetRandomItem();
                    PlayerPrefs.SetString(eventManager.eventGainItemKey, eventManager.item.title);
                    PlayerPrefs.Save();
                }
                eventManager.option2Text.text = "Pick up the item";
                eventManager.option1Text.text = "Leave it alone";

                if (eventManager.item.title == null) {
                    eventManager.eventText.text = "You find a hole where an item used to be. However, it seems like you already have all the items in the world.";
                    eventManager.FinishEvent();
                    return;
                }

                eventManager.itemRewardPanel.SetActive(true);
                eventManager.itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{eventManager.item.title}");
                eventManager.itemTitle.GetComponent<TMP_Text>().text = eventManager.item.displayTitle;
                eventManager.itemDescription.GetComponent<TMP_Text>().text = eventManager.item.description;

                eventManager.eventText.text = $"You find a mysterious item on the ground called {eventManager.item.displayTitle}. Do you want to pick it up?";
            },

            OnClickOption2 = () => {
                eventManager.item.UseImmediately(new(summoner: eventManager.summonerManager.summoner));
                ItemManager.AddItem(eventManager.item);
                eventManager.eventText.text = $"You picked up {eventManager.item.displayTitle}!";
                eventManager.itemRewardPanel.SetActive(false);
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = $"You chose to leave the item for another adventurer to find.";
                eventManager.itemRewardPanel.SetActive(false);
            },
        };

        return newEvent;
    }
}