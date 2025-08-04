public class LoseGold {
    public Event GetEvent(EventManager eventManager) {
        Event newEvent = new() {
            OnSetup = () => {
                int goldAmount = 25;
                eventManager.eventText.text = $"A thief stole {goldAmount} gold from you! You cry for a bit but then realise your time is better spent moving on.";
                GoldManager.RemoveGold(goldAmount);
            }
        };

        return newEvent;
    }
}