using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : MonoBehaviour {
    public TMP_Text title;
    public TMP_Text description;
    public Slider progressSlider;
    public TMP_Text sliderText;

    public void SetValues(string title, string description, int currentValue, int maxValue) {
        this.title.text = title;
        this.description.text = description;
        progressSlider.maxValue = maxValue;
        progressSlider.value = currentValue;

        if (currentValue >= maxValue) {
            sliderText.text = "Completed";
        } else {
            sliderText.text = $"{currentValue}/{maxValue}";
        }
    }
}