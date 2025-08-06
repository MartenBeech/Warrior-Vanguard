using UnityEngine;
public class GainMaxHealth {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = "A stranger walks up to you and gives you a potion. Do you drink it?";
                
            },

            OnAccept = () => {
                int healthDifference = 5;
                eventManager.summonerManager.GainMaxHealth(healthDifference);
                eventManager.eventText.text = $"Without hesitation, you drink the stranger's potion and gains {healthDifference} max health. You are happy about your life choices.";
            }
        };

        return newEvent;
    }
}