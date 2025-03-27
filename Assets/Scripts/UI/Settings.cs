using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour {
    public GameObject settingsObject;
    public TextMeshProUGUI gameSpeedText;
    public static int gameSpeed = 4;
    int musicVolumePercentage = 0;
    public MusicPlayer musicPlayer;
    public GameObject musicPlayerMute;
    public GameObject[] musicPlayerBars;
    public Sprite audioIcon;
    public Sprite audioIconMute;

    void Start() {
        gameSpeedText.text = $"{gameSpeed}";
        UpdateMusicVolume(musicVolumePercentage);
    }

    public void ToggleSettingsEnabled() {
        if (settingsObject.activeSelf) {
            settingsObject.SetActive(false);
        } else {
            settingsObject.SetActive(true);
        }
    }

    public void IncreaseGameSpeed() {
        if (gameSpeed >= 4) return;
        gameSpeed *= 2;
        gameSpeedText.text = $"{gameSpeed}";
    }

    public void DecreaseGameSpeed() {
        if (gameSpeed <= 1) return;
        gameSpeed /= 2;
        gameSpeedText.text = $"{gameSpeed}";
    }

    public void UpdateMusicVolume(int volumePercentage) {
        if (musicPlayer) musicPlayer.UpdateVolume(volumePercentage);

        if (volumePercentage > 0) {
            musicPlayerMute.GetComponent<Image>().sprite = audioIcon;
        } else {
            musicPlayerMute.GetComponent<Image>().sprite = audioIconMute;
        }

        for (int i = 0; i < 5; i++) {
            if (volumePercentage >= (i * 20) + 20) {
                musicPlayerBars[i].GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.black);
            } else {
                musicPlayerBars[i].GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.gray);
            }
        }
    }
}