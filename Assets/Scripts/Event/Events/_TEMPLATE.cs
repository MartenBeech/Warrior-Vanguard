public class EVENTCLASSNAME {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            //Can be deleted. Should only be used for conditional enables
            enableOption3Button = true,
            enableOption2Button = true,
            enableOption1Button = true,

            OnSetup = () => {
                eventManager.eventText.text = "";
                eventManager.option3Text.text = "Option 3 text";
                eventManager.option2Text.text = "Option 2 text";
                eventManager.option1Text.text = "Option 1 text";
            },

            OnClickOption3 = () => {
                eventManager.eventText.text = "";
            },

            OnClickOption2 = () => {
                eventManager.eventText.text = "";
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = "";
            },
        };

        return newEvent;
    }
}