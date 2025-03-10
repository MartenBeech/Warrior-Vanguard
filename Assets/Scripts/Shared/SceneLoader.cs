using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {
    public static void LoadMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadBattlefield() {
        SceneManager.LoadScene("Battlefield");
    }

    public static void LoadShop() {
        SceneManager.LoadScene("Shop");
    }

    public static void LoadMap() {
        SceneManager.LoadScene("Map");
    }

    public static void LoadCredits() {
        SceneManager.LoadScene("Credits");
    }

    public static void ExitGame() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity
#endif

        Application.Quit(); //Only works when the game is live
    }
}
