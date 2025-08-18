using System;
using UnityEngine;

public class SummonerData {
    public string title;
    public string description;
    public string heroPowerTitle;
    public string heroPowerDescription;
    public int heroPowerCost;
    public Sprite heroPowerImage;
    public Action<HeroPowerEffectParams> heroPowerEffect;
    public Character.Genre genre;
}

