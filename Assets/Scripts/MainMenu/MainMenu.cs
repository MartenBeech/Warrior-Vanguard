using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void StartNewGame() {
        PlayerPrefs.DeleteAll();
        
        //Start with 10 random cards
        for (int i = 0; i < 10; i++) {
            Card card = new();
            card.SetStats(CardDatabase.GetRandomWarriorStats());
            DeckManager.AddCard(card);
        }
        ItemManager.InitAvailableItems();

        SceneLoader.LoadMap();
    }

    public void ExitGame() {
        SceneLoader.ExitGame();
    }

    public void LoadCredits() {
        SceneLoader.LoadCredits();
    }
}
