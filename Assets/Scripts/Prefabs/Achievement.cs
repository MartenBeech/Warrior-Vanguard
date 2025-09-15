using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {
    public TMP_Text descriptionText;
    public Slider progressSlider;
    public TMP_Text sliderText;

    public void SetValues(string description, int currentValue, int maxValue) {
        descriptionText.text = description;
        progressSlider.maxValue = maxValue;
        progressSlider.value = currentValue;

        if (currentValue > maxValue) {
            sliderText.text = "Completed";
        } else {
            sliderText.text = $"{currentValue}/{maxValue}";
        }
    }
}