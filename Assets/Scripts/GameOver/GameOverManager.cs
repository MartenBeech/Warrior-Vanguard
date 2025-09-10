using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
    public TMP_Text GameOverText;
    public Image summonerImage;
    public Slider expSlider;
    public TMP_Text expText;
    public GameObject levelUpPanel;

    private void Start() {
        levelUpPanel.SetActive(false);
        if (LevelManager.isAlive) {
            GameOverText.text = "You Win! Good job!";
        } else {
            GameOverText.text = "You Lost! Sucks to be you..";
        }
        summonerImage.sprite = Resources.Load<Sprite>($"Images/Summoners/{FriendlySummoner.summonerData.title}");
        UpdateExpAnimation();
    }

    async private void UpdateExpAnimation() {
        Genre genre = FriendlySummoner.summonerData.genre;
        expText.text = $"Level {ExperienceManager.GetLevel(genre)}";

        int startValue = ExperienceManager.GetExperience(genre);
        int targetValue = startValue + ExperienceManager.GetTempExperience();
        if (targetValue >= ExperienceManager.GetXpForNextLevel(genre)) {
            targetValue = ExperienceManager.GetXpForNextLevel(genre);
        }
        ExperienceManager.AddTempExperience(startValue - targetValue); // Decrease temp XP for next level

        if (ExperienceManager.IsMaxLevel(genre)) {
            expSlider.value = expSlider.maxValue;
            return;
        }

        expSlider.value = startValue;
        expSlider.maxValue = ExperienceManager.GetXpForNextLevel(genre);

        float animationTime = 1.5f;
        float elapsedTime = 0f;

        while (elapsedTime < animationTime) {
            elapsedTime += Time.deltaTime;
            float newValue = Mathf.Lerp(startValue, targetValue, elapsedTime / animationTime);
            expSlider.value = newValue;

            await System.Threading.Tasks.Task.Yield();
        }

        expSlider.value = targetValue;
        if (targetValue >= ExperienceManager.GetXpForNextLevel(genre) && !ExperienceManager.IsMaxLevel(genre)) {
            levelUpPanel.SetActive(true);
        }
        ExperienceManager.AddExperience(genre, targetValue - startValue);
    }

    public void LevelUpContinueButtonPressed() {
        levelUpPanel.SetActive(false);
        if (ExperienceManager.GetTempExperience() > 0) {
            UpdateExpAnimation();
        }
    }

    public void LoadMainMenu() {
        SceneLoader.LoadScene(SceneLoader.Scene.MainMenu);
    }
}