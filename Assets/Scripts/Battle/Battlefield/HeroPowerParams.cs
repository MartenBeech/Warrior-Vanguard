using System;

public class HeroPowerParams {
    public string title;
    public string description;
    public int cost;
    public Action<HeroPowerEffectParams> effect;

    public HeroPowerParams(
        string title = "",
        string description = "",
        int cost = 0,
        Action<HeroPowerEffectParams> effect = null
    ) {
        this.title = title;
        this.description = description;
        this.cost = cost;
        this.effect = effect;
    }
}

public class HeroPowerEffectParams {
    public GameManager gameManager;
    public Hand friendHand;
    public WarriorSummoner warriorSummoner;
    public Summoner friendSummoner;

    public HeroPowerEffectParams(
        GameManager gameManager = null,
        Hand friendHand = null,
        WarriorSummoner warriorSummoner = null,
        Summoner friendSummoner = null
    ) {
        this.gameManager = gameManager;
        this.friendHand = friendHand;
        this.warriorSummoner = warriorSummoner;
        this.friendSummoner = friendSummoner;
    }
}