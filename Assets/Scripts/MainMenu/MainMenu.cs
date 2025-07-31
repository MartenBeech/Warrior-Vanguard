using UnityEngine;

public class MainMenu : MonoBehaviour {
    public Card card;
    public GameObject collection;
    public void StartNewGame() {
        PlayerPrefs.DeleteAll();

        //Start with 12 random cards
        for (int i = 0; i < 20; i++) {
            card.SetStats(CardDatabase.allCards[i]); //TODO: Replace this line with the one below. This adds the 12 newest created cards to your deck
            // card.SetStats(CardDatabase.GetRandomWarriorStats());
            DeckManager.AddCard(card);
        }
        ItemManager.InitAvailableItems();

        SceneLoader.LoadSummonerSelector();
    }

    public void ContinueGame() {
        ItemManager.LoadAvailableItems();
        SceneLoader.LoadMap();
    }

    public void ExitGame() {
        SceneLoader.ExitGame();
    }

    public void LoadCredits() {
        SceneLoader.LoadCredits();
    }

    public void ToggleCollection() {
        collection.SetActive(!collection.activeSelf);
    }
}
