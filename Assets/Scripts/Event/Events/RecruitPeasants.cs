using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RecruitPeasants {
    public Event GetEvent(EventManager eventManager) {
        Item item = ItemManager.GetItemByTitle("CharmOfPeasants");
        int amount = 10;
        Event newEvent = new() {
            OnSetup = () => {
                eventManager.eventText.text = $"A group of peasants wants to join your army. They will give you a powerful item if you accept.";
                eventManager.option2Text.text = "Accept the offer";
                eventManager.option1Text.text = "Reject the offer";

                eventManager.cardItemRewardPanel.SetActive(true);
                eventManager.cardItemRewardItem = item;
                eventManager.cardItemRewardItemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{item.title}");
                eventManager.cardItemRewardItemTitle.GetComponent<TMP_Text>().text = item.displayTitle;
                eventManager.cardItemRewardItemDescription.GetComponent<TMP_Text>().text = item.description;

                eventManager.cardItemRewardCard.SetStats(CardDatabase.GetStatsByTitleAndLevel("Peasant_0"));
                eventManager.cardItemRewardCard.SetHoverCardFromMap();
                eventManager.cardItemRewardCard.UpdateCardUI();
                eventManager.cardItemRewardAmountText.text = $"x{amount}";
            },

            OnClickOption2 = () => {
                ItemManager.AddItem(eventManager.cardItemRewardItem);
                for (int i = 0; i < amount; i++) {
                    eventManager.GainCard(eventManager.cardItemRewardCard);
                }

                eventManager.eventText.text = $"You accepted the offer! The peasants are happy and ready to fight for you.";
                eventManager.cardItemRewardPanel.SetActive(false);
            },

            OnClickOption1 = () => {
                eventManager.eventText.text = $"You rejected the offer! The peasants are very angry. You should probably leave.";
                eventManager.cardItemRewardPanel.SetActive(false);
            }
        };

        return newEvent;
    }
}