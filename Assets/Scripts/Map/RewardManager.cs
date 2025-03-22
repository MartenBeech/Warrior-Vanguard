using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour {
    public GameObject cardButtonPrefab;
    public Button skipButton;
    public List<Card> rewardedCards = new List<Card>();
    public GameObject rewardPanel;
    public DeckBuilder deckBuilder;

    private void Start() {
        rewardPanel.SetActive(false);
    }

    public void ShowReward(MapTile.EnemyType enemyType) {
        rewardPanel.SetActive(true);

        CardRarity rarity = enemyType == MapTile.EnemyType.Miniboss ? CardRarity.Legendary :
                            CardRarity.Common;

        foreach (Card card in rewardedCards) {
            WarriorStats stats = CardDatabase.GetRandomWarriorStats(rarity);
            card.SetStats(stats);
            card.UpdateCardUi();
        }
    }

    public void SelectCard(Card card) {
        ClosePopup();
        deckBuilder.AddCardToDeck(card);
    }

    public void SkipReward() {
        ClosePopup();
    }

    private void ClosePopup() {
        rewardPanel.SetActive(false);
    }
}
