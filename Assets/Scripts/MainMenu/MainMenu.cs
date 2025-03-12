using UnityEngine;

public class MainMenu : MonoBehaviour {

    public void StartNewGame() {
        PlayerPrefs.DeleteAll();
        
        //Start with 20 random cards
        for (int i = 0; i < 20; i++) {
            Card card = new();
            card.SetStats(CardDatabase.GetRandomWarriorStats());
            DeckManager.AddCard(card);
        }

        SceneLoader.LoadMap();
    }

    public void ExitGame() {
        SceneLoader.ExitGame();
    }

    public void LoadCredits() {
        SceneLoader.LoadCredits();
    }
}
