using UnityEngine;
public class Gambler {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            enableOption3Button = GoldManager.gold >= 100,

            OnSetup = () => {
                eventManager.eventText.text = "You find a hooded man with 3 cups in front of him. “Everything is gambling, no matter how big the chance. Care for a game?”";
                eventManager.option3Text.text = "Bet 100 gold, 50% chance to win 200 gold";
                eventManager.option2Text.text = "Bet all your gold, 99% chance to win it back";
                eventManager.option1Text.text = "Bet all your gold, 10% to win 1000 gold";
            },

            OnClickOption3 = () => {
                GoldManager.RemoveGold(100);
                if (Random.value < 0.5f) {
                    GoldManager.AddGold(200);
                    eventManager.eventText.text = "You lift the cup, which is full of gold. 200 gold to be exact! Straight down your pocket";
                } else {
                    eventManager.eventText.text = "The cup you chose is empty. Before you can say anything, the stranger is already gone.";
                }
            },

            OnClickOption2 = () => {
                int allGold = GoldManager.gold;
                GoldManager.RemoveGold(allGold);
                if (Random.value < 0.99f) {
                    GoldManager.AddGold(allGold);
                    eventManager.eventText.text = "You lift the cup, which is full of gold. Your own gold! Straight down your pocket";
                } else {
                    eventManager.eventText.text = "The cup you chose is empty. You try again, and again, and again, but still empty. At the same time your wallet feels emptier.. You are the 1%";
                }
            },

            OnClickOption1 = () => {
                GoldManager.RemoveGold(GoldManager.gold);
                if (Random.value < 0.1f) {
                    GoldManager.AddGold(1000);
                    eventManager.eventText.text = "You lift the cup, which is full of gold. 1000 gold to be exact! Straight down your pocket";
                } else {
                    eventManager.eventText.text = "The cup you chose is empty. You try again, and again, and again, but still empty. At the same time your wallet feels emptier";
                }
            },            
        };

        return newEvent;
    }
}