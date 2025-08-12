public class UndeadSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "An Undead Summoner with dark powers",
            heroPowerTitle = "Undead Armor Up",
            heroPowerDescription = "Gain 3 armor",
            heroPowerCost = 3
        };

        return data;
    }
}