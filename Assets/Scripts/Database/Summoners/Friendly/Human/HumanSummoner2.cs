using UnityEngine;

public class HumanSummoner2 {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Peasant with big dreams and big triforks!",
            heroPowerTitle = "Hold Pitch",
            heroPowerDescription = $"Summon a Peasant",
            heroPowerCost = 2,
            heroPowerEffect = async parameters => {
                WarriorStats peasant = new Peasant().GetStats();
                peasant.alignment = CharacterSpawner.Alignment.Friend;
                await parameters.characterSpawner.SpawnRandomly(peasant, parameters.friendSummoner.transform.position);
            }
        };
        data.heroPowerImage = Resources.Load<Sprite>($"Images/HeroPowers/{data.heroPowerTitle}");

        return data;
    }
}