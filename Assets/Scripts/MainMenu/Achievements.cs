using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour {
    public Transform items;
    public Achievement achievementPrefab;
    public Achievement achievement;

    void Awake() {
        //Remember to add PlayerPrefs to MainMenu.cs/DeleteTemporaryPlayerPrefs()
        CreateAchievementInstance("Beat the game with Human", ProgressHelper.GetWins(Genre.Human), 1);
        CreateAchievementInstance("Beat the game with Elves", ProgressHelper.GetWins(Genre.Elves), 1);
        CreateAchievementInstance("Beat the game with Undead", ProgressHelper.GetWins(Genre.Undead), 1);
        CreateAchievementInstance("Beat the game with Underworld", ProgressHelper.GetWins(Genre.Underworld), 1);
        CreateAchievementInstance("Win a battle with 1 HP remaining", PlayerPrefs.GetInt(PlayerPrefsKeys.survivedWith1Hp, 0), 1);
        CreateAchievementInstance("Have 10 or more friendly warriors on the board at the same time", PlayerPrefs.GetInt(PlayerPrefsKeys.tenOrMoreFriends, 0), 1);



        CreateAchievementInstance("ALL ACHIEVEMENTS AFTER THIS ARE NOT IMPLEMENTED", 0, 1);



        CreateAchievementInstance("Fight a battle that last 20+ rounds", 0, 1);
        CreateAchievementInstance("Have 3 friendly dragons on the board at the same time", 0, 1);
        CreateAchievementInstance("Have 20 or more shield during combat", 0, 1);
        CreateAchievementInstance("Have 10 or more skeleton bones during combat", 0, 1);
        CreateAchievementInstance("End your turns with at least 10 coins remaining", 0, 10);
        CreateAchievementInstance("Win a battle without losing a single warrior", 0, 1);
        CreateAchievementInstance("Win a battle after taking fatigue damage 5 times", 0, 1);
        CreateAchievementInstance("Defeat a boss without taking any damage", 0, 1);
        CreateAchievementInstance("Defeat a boss without using your hero power", 0, 1);
        CreateAchievementInstance("Defeat a boss without using any spells", 0, 1);
        CreateAchievementInstance("Beat the game with no items", 0, 1);
        CreateAchievementInstance("Beat the game with no rare of legendary cards", 0, 1);
        CreateAchievementInstance("Defeat (insert boss or miniboss here)", 0, 1);
        CreateAchievementInstance("Get Human to max level", 0, 1);
        CreateAchievementInstance("Get Elves to max level", 0, 1);
        CreateAchievementInstance("Get Undead to max level", 0, 1);
        CreateAchievementInstance("Get Underworld to max level", 0, 1);
    }

    void CreateAchievementInstance(string description, int currentValue, int maxValue) {
        Achievement achievement = Instantiate(achievementPrefab, items);
        achievement.SetValues(description, currentValue, maxValue);
    }
}