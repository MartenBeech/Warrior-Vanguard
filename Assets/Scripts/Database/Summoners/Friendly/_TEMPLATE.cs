using System.Collections.Generic;
using UnityEngine;

public class TEMPLATE_SUMMONER {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            genre = Character.Genre.None,
            description = "DESCRIPTION",
            heroPowerTitle = "HP_TITLE",
            heroPowerDescription = "HP_DESCRIPTION",
            heroPowerCost = 0,
            heroPowerEffect = parameters => {

            }
        };
        data.heroPowerImage = Resources.Load<Sprite>($"Images/HeroPowers/{data.heroPowerTitle}");

        return data;
    }
}