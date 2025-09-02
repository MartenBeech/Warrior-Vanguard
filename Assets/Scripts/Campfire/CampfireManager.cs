using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CampfireManager : MonoBehaviour {
    public GameObject restButton;
    public GameObject upgradeCardButton;
    public GameObject upgradeCardPanel;
    public GameObject cardPrefab;
    public TMP_Text infoText;
    public Transform deckListContainer;
    public DeckBuilder deckBuilder;
    public TMP_Text restButtonText;
    private int healAmount;
    private void Start() {
        upgradeCardPanel.SetActive(false);
        healAmount = (int)(FriendlySummoner.GetMaxHealth() * 0.3);
        restButtonText.text = $"Rest - Heal {healAmount} HP";
    }

    public void RestButtonClicked() {
        FriendlySummoner.GainHealth(healAmount);
        infoText.text = $"You have rested and healed {healAmount} HP";
        FinishResting();
    }

    public void UpgradeCardButtonClicked() {
        upgradeCardPanel.SetActive(true);
        foreach (Transform child in deckListContainer) {
            Destroy(child.gameObject);
        }

        foreach (WarriorStats stats in DeckManager.GetDeck()) {
            if (stats.level > 0) continue;
            GameObject cardItem = Instantiate(cardPrefab, deckListContainer);
            cardItem.transform.localScale = new Vector2(1.5f, 1.5f);
            cardItem.GetComponent<DragDrop>().enabled = false;
            cardItem.GetComponent<ObjectAnimation>().enabled = false;
            Card card = cardItem.GetComponent<Card>();
            card.SetStats(stats);
            card.UpdateCardUI();
            card.GetComponent<Button>().onClick.AddListener(() => UpgradeCard(card));
        }
    }

    public void CloseUpgradeCardPanel() {
        upgradeCardPanel.SetActive(false);
    }

    private void UpgradeCard(Card card) {
        deckBuilder.UpgradeCardInDeck(card);
        upgradeCardPanel.SetActive(false);
        infoText.text = $"{card.stats.title} has been upgraded!";
        FinishResting();
    }

    public void ReturnButtonClicked() {
        TileCompleter.MarkTileAsCompleted();
        SceneLoader.LoadMap();
    }

    private void FinishResting() {
        TileCompleter.MarkTileAsCompleted();
        restButton.SetActive(false);
        upgradeCardButton.SetActive(false);
    }
}