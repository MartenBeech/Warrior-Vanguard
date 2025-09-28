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
        CreateAchievementInstance("The Peasants Never Bothered Me Anyway", "Get Human to max level", ExperienceManager.GetLevel(Genre.Human), ExperienceManager.maxLevel);
        CreateAchievementInstance("Figting For The Forest", "Get Elves to max level", ExperienceManager.GetLevel(Genre.Elves), ExperienceManager.maxLevel);
        CreateAchievementInstance("Graveyard Enjoyer", "Get Undead to max level", ExperienceManager.GetLevel(Genre.Undead), ExperienceManager.maxLevel);
        CreateAchievementInstance("IMPressive Conquering", "Get Underworld to max level", ExperienceManager.GetLevel(Genre.Underworld), ExperienceManager.maxLevel);
        CreateAchievementInstance("Adrenaline Rush", "Win a battle with 1 HP remaining", PlayerPrefs.GetInt(PlayerPrefsKeys.adrenalineRush, 0), 1);
        CreateAchievementInstance("Swarm!", "Have 10 or more friendly warriors on the board at the same time", PlayerPrefs.GetInt(PlayerPrefsKeys.swarm, 0), 1);
        CreateAchievementInstance("Controlling The Battlefield", "Fight a battle that last 20+ rounds", PlayerPrefs.GetInt(PlayerPrefsKeys.controllingTheBattlefield), 1);
        CreateAchievementInstance("Triflame", "Have 3 friendly dragons on the board at the same time", PlayerPrefs.GetInt(PlayerPrefsKeys.triFlame), 1);
        CreateAchievementInstance("Safety First", "Have 20 or more shield during combat", PlayerPrefs.GetInt(PlayerPrefsKeys.safetyFirst), 1);
        CreateAchievementInstance("Spooky Scary Skeletons", "Have 10 or more skeleton bones during combat", PlayerPrefs.GetInt(PlayerPrefsKeys.spookyScarySkeletons), 1);
        CreateAchievementInstance("Poor Looter", "Beat the game with no items", PlayerPrefs.GetInt(PlayerPrefsKeys.poorLooter), 1);
        CreateAchievementInstance("These Were Easier To Find", "Beat the game with no rare or legendary cards", PlayerPrefs.GetInt(PlayerPrefsKeys.theseWereEasierToFind), 1);
        CreateAchievementInstance("Careful Spender", "End 10 turns with at least 10 coins remaining", PlayerPrefs.GetInt(PlayerPrefsKeys.carefulSpender), 10);
        CreateAchievementInstance("Save Every Resource", "Win a battle without losing a single warrior", PlayerPrefs.GetInt(PlayerPrefsKeys.saveEveryResource), 1);
        CreateAchievementInstance("Living On The Edge", "Win a battle after taking fatigue damage 5 times", PlayerPrefs.GetInt(PlayerPrefsKeys.livingOnTheEdge), 1);
        CreateAchievementInstance("Flawless", "Defeat a boss without taking any damage", PlayerPrefs.GetInt(PlayerPrefsKeys.flawless), 1);
        CreateAchievementInstance("Hero Power Deactivated", "Defeat a boss without using your hero power", PlayerPrefs.GetInt(PlayerPrefsKeys.heroPowerDeactivated), 1);


        CreateAchievementInstance("PLACEHOLDER", "ALL ACHIEVEMENTS AFTER THIS ARE NOT IMPLEMENTED", 0, 1);


        //TODO: Implement these ideas (Or remove them if they suck!)
        CreateAchievementInstance("Warriors All The Way", "Defeat a boss without using any spells", 0, 1);
        CreateAchievementInstance("", "Defeat (insert boss or miniboss here)", 0, 1);
        
    }

    void CreateAchievementInstance(string title, string description, int currentValue, int maxValue) {
        Achievement achievement = Instantiate(achievementPrefab, items);
        achievement.SetValues(title, description, currentValue, maxValue);
    }
}