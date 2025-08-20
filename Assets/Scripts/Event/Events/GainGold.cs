public class GainGold {
    public Event GetEvent(EventManager eventManager) {
        int goldAmount = 50;
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = $"You found treasure! Inside is {goldAmount} gold! Do you take it?";
                eventManager.option1Text.text = "Accept";
            },

            OnClickOption1 = () => {
                GoldManager.AddGold(goldAmount);
                eventManager.eventText.text = "Congratulations! You are now a bit richer than before.";
            }
        };

        return newEvent;
    }
}