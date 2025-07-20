using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SummonerStats {
    public string title;
    public string displayTitle;
    public string description;
    public bool isFriendly;
    public int health;
    public int healthMax;
    public int shield;
    public int skeletonBones = 0;
    public int difficulty = 1;
    public WarriorAbility ability = new();

    public SummonerStats(string title, int health, int healthMax, bool isFriendly) {
        this.title = PlayerPrefs.GetString("SelectedSummoner");
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        description = title switch {
            "HumanSummoner" => "A Human Summoner with balanced stats.",
            "ElvenSummoner" => "An Elven Summoner with agility and magic.",
            "UndeadSummoner" => "An Undead Summoner with dark powers.",
            "UnderworldSummoner" => "A Summoner from the Underworld with unique abilities.",
            _ => "Just your friendly neighborhood Summoner.",
        };
        this.health = health;
        this.healthMax = healthMax;
        shield = 0;
    }

    public SummonerStats() {
    }

    public void SetStats(SummonerStats stats) {
        title = stats.title;
        displayTitle = Regex.Replace(stats.title, "(?<!^)([A-Z])", " $1");
        description = stats.description;
        health = stats.health;
        healthMax = stats.healthMax;
        shield = stats.shield;
    }
}