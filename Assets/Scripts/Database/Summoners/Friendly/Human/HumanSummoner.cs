public class HumanSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "A Human Summoner with balanced stats"
        };

        return data;
    }
}