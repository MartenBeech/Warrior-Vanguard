using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadBattlefield() {
        SceneManager.LoadScene("Battlefield");
    }

    public void LoadMap() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Map");
    }

    public void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity
#endif

        Application.Quit(); //Only works when the game is live
    }

    public void LoadCredits() {
        SceneManager.LoadScene("Credits");
    }
}
