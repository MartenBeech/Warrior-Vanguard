using UnityEngine;
public class HealthTrade {
    public Event GetEvent(EventManager eventManager) {
        int loseHealthAmount = 25;
        int gainMaxHealthAmount = 10;
        Event newEvent = new() {
            enableOption2Button = FriendlySummoner.currentHealth > loseHealthAmount,

            OnSetup = () => {
                eventManager.eventText.text = "You find a strange altar. A note reads: 'A sacrifice of blood will give you strength'. Do you proceed?";
                eventManager.option2Text.text = $"Lose {loseHealthAmount} health, gain {gainMaxHealthAmount} max health";
                eventManager.option1Text.text = "Return";
            },

            OnClickOption2 = () => {
                eventManager.summonerManager.LoseHealth(loseHealthAmount);
                eventManager.summonerManager.GainMaxHealth(gainMaxHealthAmount);
                eventManager.eventText.text = "You feel weaker, but know your potential grows stronger.";
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = "You feel exactly the same as before.";
            },
        };

        return newEvent;
    }
}