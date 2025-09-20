using System.Collections.Generic;
using UnityEngine;

public class Achievements : MonoBehaviour {
    public Transform items;
    public Achievement achievementPrefab;
    public Achievement achievement;

    void Awake() {
        //Remember to add PlayerPrefs to MainMenu.cs/DeleteTemporaryPlayerPrefs()
        CreateAchievementInstance("Raise The Flags", "Beat the game with Human", ProgressHelper.GetWins(Genre.Human), 1);
        CreateAchievementInstance("Sound The Horn", "Beat the game with Elves", ProgressHelper.GetWins(Genre.Elves), 1);
        CreateAchievementInstance("Winning From Beyond The Grave", "Beat the game with Undead", ProgressHelper.GetWins(Genre.Undead), 1);
        CreateAchievementInstance("World On Fire", "Beat the game with Underworld", ProgressHelper.GetWins(Genre.Underworld), 1);
        CreateAchievementInstance("Adrenaline Rush", "Win a battle with 1 HP remaining", PlayerPrefs.GetInt(PlayerPrefsKeys.adrenalineRush, 0), 1);
        CreateAchievementInstance("Swarm!", "Have 10 or more friendly warriors on the board at the same time", PlayerPrefs.GetInt(PlayerPrefsKeys.swarm, 0), 1);
        CreateAchievementInstance("Controlling The Battlefield", "Fight a battle that last 20+ rounds", PlayerPrefs.GetInt(PlayerPrefsKeys.controllingTheBattlefield), 1);
        CreateAchievementInstance("Triflame", "Have 3 friendly dragons on the board at the same time", PlayerPrefs.GetInt(PlayerPrefsKeys.triFlame), 1);
        CreateAchievementInstance("Safety First", "Have 20 or more shield during combat", PlayerPrefs.GetInt(PlayerPrefsKeys.safetyFirst), 1);



        CreateAchievementInstance("PLACEHOLDER", "ALL ACHIEVEMENTS AFTER THIS ARE NOT IMPLEMENTED", 0, 1);


        //TODO: Implement these ideas (Or remove them if they suck!)
        CreateAchievementInstance("Spooky Scary Skeletons", "Have 10 or more skeleton bones during combat", 0, 1);
        CreateAchievementInstance("Careful Spender", "End your turns with at least 10 coins remaining", 0, 10);
        CreateAchievementInstance("Save Every Resource", "Win a battle without losing a single warrior", 0, 1);
        CreateAchievementInstance("Living On The Edge", "Win a battle after taking fatigue damage 5 times", 0, 1);
        CreateAchievementInstance("Flawless", "Defeat a boss without taking any damage", 0, 1);
        CreateAchievementInstance("Hero Power Deactivated", "Defeat a boss without using your hero power", 0, 1);
        CreateAchievementInstance("Warriors All The Way", "Defeat a boss without using any spells", 0, 1);
        CreateAchievementInstance("Poor Looter", "Beat the game with no items", 0, 1);
        CreateAchievementInstance("These Were Easier To Find", "Beat the game with no rare of legendary cards", 0, 1);
        CreateAchievementInstance("", "Defeat (insert boss or miniboss here)", 0, 1);
        CreateAchievementInstance("The Peasants Never Bothered Me Anyway", "Get Human to max level", 0, 1);
        CreateAchievementInstance("Figting For The Forest", "Get Elves to max level", 0, 1);
        CreateAchievementInstance("Graveyard Enjoyer", "Get Undead to max level", 0, 1);
        CreateAchievementInstance("IMPressive Conquering", "Get Underworld to max level", 0, 1);
    }

    void CreateAchievementInstance(string title, string description, int currentValue, int maxValue) {
        Achievement achievement = Instantiate(achievementPrefab, items);
        achievement.SetValues(title, description, currentValue, maxValue);
    }
}