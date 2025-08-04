using UnityEngine;
public class LoseHealth {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                int healthDifference = 5;
                eventManager.eventText.text = $"An unfriendly skeleton walks up to you and hit you in the face. You leave with {healthDifference} less health and a lot of questions.";
                eventManager.summonerManager.LoseHealth(healthDifference);
            },
        };

        return newEvent;
    }
}