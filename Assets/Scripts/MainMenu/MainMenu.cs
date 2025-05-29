using UnityEngine;

public class MainMenu : MonoBehaviour {
    public Card card;
    public void StartNewGame() {
        PlayerPrefs.DeleteAll();

        //Start with 20 random cards
        for (int i = 0; i < 12; i++) {
            card.SetStats(CardDatabase.allCards[i]); //TODO: Replace this line with the one below. This adds the 12 newest created cards to your deck
            // card.SetStats(CardDatabase.GetRandomWarriorStats());
            DeckManager.AddCard(card);
        }
        ItemManager.InitAvailableItems();

        SceneLoader.LoadMap();
    }

    public void ContinueGame() {
        SceneLoader.LoadMap();
    }

    public void ExitGame() {
        SceneLoader.ExitGame();
    }

    public void LoadCredits() {
        SceneLoader.LoadCredits();
    }
}
