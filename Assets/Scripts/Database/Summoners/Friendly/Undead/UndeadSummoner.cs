public class UndeadSummoner {
    public SummonerData GetData() {
        SummonerData data = new() {
            title = GetType().Name,
            description = "An Undead Summoner with dark powers"
        };

        return data;
    }
}