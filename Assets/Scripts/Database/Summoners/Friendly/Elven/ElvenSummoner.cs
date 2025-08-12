public class ElvenSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "An Elven Summoner with agility and magic",
            heroPowerTitle = "Elven Armor Up",
            heroPowerDescription = "Gain 2 armor",
            heroPowerCost = 2
        };

        return data;
    }
}