public class UnderworldSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Summoner from the Underworld with unique abilities",
            heroPowerTitle = "Dragon Armor Up",
            heroPowerDescription = "Gain 4 armor",
            heroPowerCost = 4
        };

        return data;
    }
}