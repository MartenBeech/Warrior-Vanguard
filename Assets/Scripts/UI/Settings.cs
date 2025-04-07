using UnityEngine;

public class Settings : MonoBehaviour {
    public static int gameSpeed = 4;
    public static int musicVolumePercentage = 0;
    public MusicPlayer musicPlayer;

    void Start() {
        UpdateMusicVolume(musicVolumePercentage);
    }

    public void UpdateMusicVolume(int volumePercentage) {
        musicVolumePercentage = volumePercentage;
        if (musicPlayer) musicPlayer.UpdateVolume(volumePercentage);
    }
}