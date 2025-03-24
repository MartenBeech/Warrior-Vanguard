using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public AudioSource audioSource;

    public void UpdateVolume(float volumePercentage) {
        audioSource.volume = volumePercentage / 100f;
    }
}