using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{
    public GameObject settingsObject;
    public TextMeshProUGUI gameSpeedText;
    public static int gameSpeed = 4;

    void Start()
    {
        gameSpeedText.text = $"{gameSpeed}";
    }

    public void ToggleSettingsEnabled()
    {
        if (settingsObject.activeSelf)
        {
            settingsObject.SetActive(false);
        }
        else
        {
            settingsObject.SetActive(true);
        }
    }

    public void IncreaseGameSpeed()
    {
        if (gameSpeed >= 4) return;
        gameSpeed *= 2;
        gameSpeedText.text = $"{gameSpeed}";
    }

    public void DecreaseGameSpeed()
    {
        if (gameSpeed <= 1) return;
        gameSpeed /= 2;
        gameSpeedText.text = $"{gameSpeed}";
    }
}