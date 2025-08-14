public class HumanSummoner2 {
    public SummonerData GetData() {
        int armorGain = 2;
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Peasant with big dreams and big triforks!",
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