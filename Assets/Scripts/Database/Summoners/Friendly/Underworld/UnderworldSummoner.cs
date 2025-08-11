public class UnderworldSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Summoner from the Underworld with unique abilities"
        };

        return data;
    }
}