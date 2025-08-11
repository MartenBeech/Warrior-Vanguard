public class ElvenSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "An Elven Summoner with agility and magic"
        };

        return data;
    }
}