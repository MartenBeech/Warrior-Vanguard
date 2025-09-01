using UnityEngine;
public class CursedAltar {
    public Event GetEvent(EventManager eventManager) {
        int loseHealthAmount = 10;
        Event newEvent = new() {
            enableOption2Button = FriendlySummoner.currentHealth > loseHealthAmount,

            OnSetup = () => {
                eventManager.eventText.text = "You find a strange altar. If you sacrifice enough blood, you will be rewarded. Do you proceed?";
                eventManager.option2Text.text = $"Lose {loseHealthAmount} health, 50% chance to gain an item";
                eventManager.option1Text.text = "Return";
                eventManager.eventPanels = 4;
            },

            OnClickOption2 = () => {
                eventManager.summonerManager.LoseHealth(loseHealthAmount);
                switch (eventManager.eventPanels) {
                    case 4:
                    case 3:
                        if (Random.value < 0.5f) {
                            ItemManager.AddItem(ItemManager.GetRandomItem());
                        }
                        break;
                    case 2:
                        if (Random.value < 0.5f) {
                            ItemManager.AddItem(ItemManager.GetRandomItem());
                        }
                        loseHealthAmount = 20;
                        eventManager.option2Text.text = $"Lose {loseHealthAmount} health, 100% chance to gain a powerful item";
                        break;
                    case 1:
                        //TODO: Insert Powerful item here
                        ItemManager.AddItem(ItemManager.GetRandomItem());
                        break;
                }

                eventManager.eventPanels--;
                eventManager.option2Button.GetComponent<UnityEngine.UI.Button>().interactable = FriendlySummoner.currentHealth > loseHealthAmount;
            },

            OnClickOption1 = () => {
                eventManager.eventPanels = 0;
                eventManager.eventText.text = "You step away from the altar.";
            },
        };

        return newEvent;
    }
}