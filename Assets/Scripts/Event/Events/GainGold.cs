public class GainGold {
    public Event GetEvent(EventManager eventManager) {
        int goldAmount = 50;
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = $"You found two treasure chests! One is open {goldAmount} gold, the other is closed, but contains 1-125 gold. Which do you choose?";
                eventManager.option2Text.text = $"Choose the open chest ({goldAmount} gold)";
                eventManager.option1Text.text = "Choose the closed chest (1-100 gold)";
            },

            OnClickOption2 = () => {
                GoldManager.AddGold(goldAmount);
                eventManager.eventText.text = "Congratulations! You are now a bit richer than before.";
            },

            OnClickOption1 = () => {
                int randomGold = UnityEngine.Random.Range(1, 126);
                GoldManager.AddGold(randomGold);
                eventManager.eventText.text = $"Congratulations! You managed to find {randomGold} gold";
            },
        };

        return newEvent;
    }
}