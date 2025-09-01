public class GainMaxHealth {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "A strange man walks up to you and offers you a potion. He claims it will increase your potential. Do you drink it?";
                eventManager.option2Text.text = "Drink it";
                eventManager.option1Text.text = "Leave it alone";
            },

            OnClickOption2 = () => {
                int healthDifference = 5;
                eventManager.summonerManager.GainMaxHealth(healthDifference);
                eventManager.eventText.text = $"Without hesitation, you drink the stranger's potion and gains {healthDifference} max health. You are happy about your life choices.";
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = $"You were raised right.. Missing out on what could have caused you to become even more powerful, but right..";
            }
        };

        return newEvent;
    }
}