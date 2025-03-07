using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapTile : MonoBehaviour {
    private bool isInteractable = false;
    public Button battleButton;
    public GameObject checkmark;


    private void Start() {
        battleButton.onClick.AddListener(GoToBattlefield);
        checkmark.SetActive(false);
    }

    public void SetInteractable(bool state) {
        isInteractable = state;
        battleButton.interactable = state;
    }

    public void MarkAsCompleted() {
        checkmark.SetActive(true);
    }

    public void GoToBattlefield() {
        if (isInteractable) {
            LevelManager.SetCurrentTile(this);
            SceneManager.LoadScene("Battlefield");
        }
    }
}
