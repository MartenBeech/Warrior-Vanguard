using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour {
    string rewardedCardsKey = "rewardedCards";
    public GameObject cardButtonPrefab;
    public Button skipButton;
    public List<Card> rewardedCards = new List<Card>();
    public GameObject rewardPanel;
    public DeckBuilder deckBuilder;

    private void Start() {
        rewardPanel.SetActive(false);
    }

    public void ShowReward(MapTile.EnemyType enemyType) {
        if (!LevelManager.isAlive || enemyType == MapTile.EnemyType.Boss) {
            SceneLoader.LoadGameOver();
            return;
        }

        rewardPanel.SetActive(true);

        // If saved in local storage, load the cards from there, else generate new ones
        if (PlayerPrefs.HasKey(rewardedCardsKey)) {
            LoadRewardOptions();
        } else {
            CardRarity rarity = enemyType == MapTile.EnemyType.Miniboss ? CardRarity.Legendary :
                                        CardRarity.Common;

            foreach (Card card in rewardedCards) {
                WarriorStats stats = CardDatabase.GetRandomWarriorStats(rarity);
                card.SetStats(stats);
                card.UpdateCardUi();
            }
        }

        SaveRewardOptions();
    }

    public void SelectCard(Card card) {
        deckBuilder.AddCardToDeck(card);
        PlayerPrefs.SetInt($"RewardChosen", 1);
        PlayerPrefs.DeleteKey(rewardedCardsKey);
        PlayerPrefs.Save();
        ClosePopup();
    }

    private void SaveRewardOptions() {
        List<string> cardTitlesAndLevels = new();
        foreach (Card card in rewardedCards) {
            cardTitlesAndLevels.Add($"{card.stats.title}_{card.stats.level}");
        }

        string cardData = string.Join(",", cardTitlesAndLevels);
        PlayerPrefs.SetString(rewardedCardsKey, cardData);
        PlayerPrefs.Save();
    }

    private void LoadRewardOptions() {
        string cardData = PlayerPrefs.GetString(rewardedCardsKey);
        string[] cardTitlesAndLevels = cardData.Split(',');

        for (int i = 0; i < rewardedCards.Count; i++) {
            WarriorStats stats = CardDatabase.GetStatsByTitleAndLevel(cardTitlesAndLevels[i]);
            rewardedCards[i].SetStats(stats);
            rewardedCards[i].UpdateCardUi();
        }
    }

    public void SkipReward() {
        PlayerPrefs.SetInt($"RewardChosen", 1);
        PlayerPrefs.DeleteKey(rewardedCardsKey);
        PlayerPrefs.Save();
        ClosePopup();
    }

    private void ClosePopup() {
        rewardPanel.SetActive(false);
    }
}
