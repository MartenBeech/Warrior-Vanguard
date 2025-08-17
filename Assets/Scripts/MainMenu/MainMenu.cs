using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject collection;
    public void StartNewGame() {
        PlayerPrefs.DeleteAll();
        SceneLoader.LoadSummonerSelector();
    }

    public void ContinueGame() {
        ItemManager.LoadAvailableItems();
        ContinueManager.LoadSummoner();
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
