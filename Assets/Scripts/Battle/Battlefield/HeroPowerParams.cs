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

    public HeroPowerEffectParams(
        GameManager gameManager = null
    ) {
        this.gameManager = gameManager;
    }
}