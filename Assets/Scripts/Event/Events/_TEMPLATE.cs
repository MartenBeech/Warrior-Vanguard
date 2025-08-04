using UnityEngine;
public class EVENTCLASSNAME {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "";
            },

            OnAccept = () => {
                eventManager.eventText.text = "";
            }
        };

        return newEvent;
    }
}