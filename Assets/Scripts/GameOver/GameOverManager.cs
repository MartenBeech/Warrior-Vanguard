using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameOver : MonoBehaviour {
    public TMP_Text GameOverText;
    private void Start() {
        if (LevelManager.isAlive) {
            GameOverText.text = "You Win! Good job!";
        } else {
            GameOverText.text = "You Lost! Sucks to be you..";
        }
    }
    
    public void LoadMainMenu() {
        SceneLoader.LoadMainMenu();
    }
}