using UnityEngine;

public class UnderworldSummoner1 {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Summoner from the Underworld with unique abilities",
            heroPowerTitle = "Mystic Gate",
            heroPowerDescription = "Summon a random 2-cost warrior",
            heroPowerCost = 4,
            heroPowerEffect = async parameters => {
                WarriorStats randomWarrior = CardDatabase.GetRandomWarriorWithSpecificCost(2, CharacterSpawner.Alignment.Friend);
                await parameters.characterSpawner.SpawnRandomly(randomWarrior, parameters.friendSummoner.transform.position);
            }
        };
        data.heroPowerImage = Resources.Load<Sprite>($"Images/HeroPowers/{data.heroPowerTitle}");

        return data;
    }
}