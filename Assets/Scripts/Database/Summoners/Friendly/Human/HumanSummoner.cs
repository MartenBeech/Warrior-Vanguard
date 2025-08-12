public class HumanSummoner {
    public SummonerData GetData() {
        int armorGain = 2;
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Human Summoner with balanced stats",
            heroPowerTitle = "Armor Up",
            heroPowerDescription = $"Gain {armorGain} armor",
            heroPowerCost = 2,
            heroPowerEffect = async parameters => {
                await parameters.gameManager.friendSummoner.AddShield(armorGain);
            }
        };

        return data;
    }
}