using System;
using UnityEngine;

public class UndeadSummoner2 {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            genre = Genre.Undead,
            description = "An Undead Summoner with dark powers",
            heroPowerTitle = "Rising Up",
            heroPowerDescription = "Add a friend to your hand that died this game",
            heroPowerCost = 3,
            heroPowerEffect = async parameters => {

                WarriorStats warrior = Rng.Entry(parameters.friendSummoner.stats.graveyard);
                if (warrior == null) return;

                Type type = Type.GetType(warrior.title);
                object instance = Activator.CreateInstance(type);
                WarriorStats stats = (WarriorStats)type.GetMethod("GetStats")?.Invoke(instance, null);

                await parameters.friendHand.MoveNewCardToHand(stats, parameters.friendSummoner.transform.position);

            }
        };
        data.heroPowerImage = Resources.Load<Sprite>($"Images/HeroPowers/{data.heroPowerTitle}");

        return data;
    }
}