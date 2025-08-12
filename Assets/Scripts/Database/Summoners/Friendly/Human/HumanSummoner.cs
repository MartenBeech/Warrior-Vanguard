public class HumanSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Human Summoner with balanced stats",
            heroPowerTitle = "Human Armor Up",
            heroPowerDescription = "Gain 1 armor",
            heroPowerCost = 1
        };

        return data;
    }
}