using System.Collections.Generic;

public class ElvenSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
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

        return data;
    }
}