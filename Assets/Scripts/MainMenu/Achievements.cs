using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour {
    public Transform items;
    public Achievement achievementPrefab;
    public Achievement achievement;

    void Awake() {
        CreateAchievementInstance("Win 10 games", ExperienceManager.GetWins(), 10);
        CreateAchievementInstance("Win 20 games", ExperienceManager.GetWins(), 20);
        CreateAchievementInstance("Win 50 games", ExperienceManager.GetWins(), 50);
        CreateAchievementInstance("Win 100 games", ExperienceManager.GetWins(), 100);
    }

    void CreateAchievementInstance(string description, int currentValue, int maxValue) {
        Achievement achievement = Instantiate(achievementPrefab, items);
        achievement.SetValues(description, currentValue, maxValue);
    }
}