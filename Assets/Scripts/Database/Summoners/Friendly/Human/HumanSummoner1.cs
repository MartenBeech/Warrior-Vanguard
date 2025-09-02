using UnityEngine;

public class HumanSummoner1 {
    public SummonerData GetData() {
        int armorGain = 2;
        SummonerData data = new() {
            title = GetType().Name,
            genre = Warrior.Genre.Human,
            description = "A Human Summoner focused on defense and resilience",
            heroPowerTitle = "Armor Up",
            heroPowerDescription = $"Gain {armorGain} armor",
            heroPowerCost = 2,
            heroPowerEffect = async parameters => {
                await parameters.gameManager.friendSummoner.AddShield(armorGain);
            }
        };
        data.heroPowerImage = Resources.Load<Sprite>($"Images/HeroPowers/{data.heroPowerTitle}");

        return data;
    }
}