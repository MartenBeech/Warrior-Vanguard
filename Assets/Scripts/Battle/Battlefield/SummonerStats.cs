using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class SummonerStats {
    public string title;
    public string displayTitle;
    public string description;
    public string heroPowerTitle;
    public string heroPowerDescription;
    public int heroPowerCost = 0;
    public Action<HeroPowerEffectParams> heroPowerEffect;
    public bool isFriendly;
    public int health;
    public int healthMax;
    public int shield = 0;
    public int difficulty = 1;
    public int skeletonBones = 0;
    public List<string> graveyard = new();
    public WarriorAbility ability = new();

    public SummonerStats(string title, int health, int healthMax) {
        this.title = FriendlySummoner.summonerData.title;
        displayTitle = Regex.Replace(title, "(?<!^)([A-Z])", " $1");
        description = FriendlySummoner.summonerData.description;
        heroPowerTitle = FriendlySummoner.summonerData.heroPowerTitle;
        heroPowerDescription = FriendlySummoner.summonerData.heroPowerDescription;
        heroPowerCost = FriendlySummoner.summonerData.heroPowerCost;
        heroPowerEffect = FriendlySummoner.summonerData.heroPowerEffect;
        this.health = health;
        this.healthMax = healthMax;
    }

    public SummonerStats() {
    }

    public void SetStats(SummonerStats stats) {
        title = stats.title;
        displayTitle = Regex.Replace(stats.title, "(?<!^)([A-Z])", " $1");
        description = stats.description;
        heroPowerTitle = stats.heroPowerTitle;
        heroPowerDescription = stats.heroPowerDescription;
        heroPowerCost = stats.heroPowerCost;
        heroPowerEffect = stats.heroPowerEffect;
        health = stats.health;
        healthMax = stats.healthMax;
        shield = stats.shield;
        difficulty = stats.difficulty;
        skeletonBones = stats.skeletonBones;
    }
}