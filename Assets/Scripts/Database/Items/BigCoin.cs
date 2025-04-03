public class BigCoin : Item {
    public BigCoin() {
        title = "Big Coin";
        description = "Immediately gain 200 gold";
    }

    public override void UseImmediately() {
        GoldManager.AddGold(200);
    }
}