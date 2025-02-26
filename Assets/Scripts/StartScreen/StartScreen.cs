using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void LoadBattlefield()
    {
        SceneManager.LoadScene("BattleField");
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stops play mode in Unity
        #endif

        Application.Quit(); //Only works when the game is live
    }
}
