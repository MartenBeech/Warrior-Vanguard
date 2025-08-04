using UnityEngine;
public class GainMaxHealth {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                int healthDifference = 5;
                eventManager.eventText.text = $"A stranger walks up to you and gives you a potion. Without hesitation, you drink it and gains {healthDifference} max health. You are happy about your life choices.";
                eventManager.summonerManager.GainMaxHealth(healthDifference);
            },
        };

        return newEvent;
    }
}