using UnityEngine;
public class WhaleHunter {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {

            OnSetup = () => {
                eventManager.eventText.text = "From the coastline you see a large whale in the waters which have been terrorizing the local surfers. You can choose to take on this monstrosity during your next battle. If you refuse… I don’t think the whale cares";
                eventManager.option2Text.text = "Go on the whale hunt";
                eventManager.option1Text.text = "Refuse";
            },

            OnClickOption2 = () => {
                eventManager.eventText.text = "Bring it on!";
                ItemManager.AddItem(ItemManager.GetItemByTitle("WhaleHunt"));
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = "As you expected, the whale doesn’t care";
            },
        };

        return newEvent;
    }
}