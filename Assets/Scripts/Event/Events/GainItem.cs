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

                if (eventManager.item.title == null) {
                    eventManager.eventText.text = "You find a hole where an item used to be. However, it seems like you already have all the items in the world.";
                    return;
                }

                eventManager.itemRewardPanel.SetActive(true);
                eventManager.itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{eventManager.item.title}");
                eventManager.itemTitle.GetComponent<TMP_Text>().text = eventManager.item.displayTitle;
                eventManager.itemDescription.GetComponent<TMP_Text>().text = eventManager.item.description;

                eventManager.eventText.text = $"You find a mysterious item on the ground called {eventManager.item.displayTitle}. Do you want to pick it up?";
            },

            OnClickOption1 = () => {
                eventManager.item.UseImmediately();
                ItemManager.AddItem(eventManager.item);
                eventManager.eventText.text = $"You picked up {eventManager.item.displayTitle}!";
                eventManager.itemRewardPanel.SetActive(false);
            }
        };

        return newEvent;
    }
}