using UnityEngine;

public class MainMenu : MonoBehaviour {
    public GameObject collection;
    public void StartNewGame() {
        PlayerPrefs.DeleteAll();
        SceneLoader.LoadScene(SceneLoader.Scene.SummonerSelector);
    }

    public void ContinueGame() {
        ItemManager.LoadAvailableItems();
        ContinueManager.LoadSummoner();
        SceneLoader.LoadScene(SceneLoader.Scene.Map);
    }

    public void ExitGame() {
        SceneLoader.ExitGame();
    }

    public void LoadCredits() {
        SceneLoader.LoadScene(SceneLoader.Scene.Credits);
    }

    public void ToggleCollection() {
        collection.SetActive(!collection.activeSelf);
    }
}
