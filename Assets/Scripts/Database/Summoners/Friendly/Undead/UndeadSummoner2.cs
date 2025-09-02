using System;
using UnityEngine;

public class UndeadSummoner2 {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            genre = Warrior.Genre.Undead,
            description = "An Undead Summoner with dark powers",
            heroPowerTitle = "Rising Up",
            heroPowerDescription = "Add a friend to your hand that died this game",
            heroPowerCost = 3,
            heroPowerEffect = async parameters => {

                string title = Rng.Entry(parameters.friendSummoner.stats.graveyard);
                if (title == null) return;

                Type type = Type.GetType(title);
                object instance = Activator.CreateInstance(type);
                WarriorStats stats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);

                await parameters.friendHand.MoveNewCardToHand(stats, parameters.friendSummoner.transform.position);

            }
        };
        data.heroPowerImage = Resources.Load<Sprite>($"Images/HeroPowers/{data.heroPowerTitle}");

        return data;
    }
}