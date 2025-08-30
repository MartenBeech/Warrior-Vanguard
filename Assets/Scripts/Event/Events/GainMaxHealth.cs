public class GainMaxHealth {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "A stranger walks up to you and gives you a potion. Do you drink it?";
                eventManager.option1Text.text = "Drink it";
                eventManager.option2Text.text = "Leave it alone";
            },

            OnClickOption1 = () => {
                int healthDifference = 5;
                eventManager.summonerManager.GainMaxHealth(healthDifference);
                eventManager.eventText.text = $"Without hesitation, you drink the stranger's potion and gains {healthDifference} max health. You are happy about your life choices.";
            },

            OnClickOption2 = () => {
                eventManager.eventText.text = $"You were raised right.. Missing out on what could have caused you to become even more powerful, but right..";
            }
        };

        return newEvent;
    }
}