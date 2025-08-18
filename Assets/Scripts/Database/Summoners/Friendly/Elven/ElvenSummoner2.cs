using System.Collections.Generic;
using UnityEngine;

public class ElvenSummoner2 {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            genre = Character.Genre.Forest,
            description = "An Elven Summoner with agility and magic",
            heroPowerTitle = "Ancient Guidance",
            heroPowerDescription = "Your most expensive cards in hand cost 1 less",
            heroPowerCost = 2,
            heroPowerEffect = parameters => {
                int highestCostCard = 0;
                List<Card> cardsInHand = parameters.friendHand.GetCardsInHand();
                foreach (var card in cardsInHand) {
                    if (card.stats.GetCost() > highestCostCard) {
                        highestCostCard = card.stats.GetCost();
                    }
                }

                foreach (var card in cardsInHand) {
                    if (card.stats.GetCost() == highestCostCard) {
                        parameters.friendHand.ReduceCostCard(card, 1);
                    }
                }
            }
        };
        data.heroPowerImage = Resources.Load<Sprite>($"Images/HeroPowers/{data.heroPowerTitle}");

        return data;
    }
}