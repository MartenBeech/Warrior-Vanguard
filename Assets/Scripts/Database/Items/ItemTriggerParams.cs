using UnityEngine;

public class ItemTriggerParams {
    public WarriorStats stats;
    public Vector2 gridIndex;
    public Summoner summoner;
    public Deck ownDeck;
    public Deck enemyDeck;
    public Hand enemyHand;
    public Coin coin;
    public GridManager gridManager;

    public ItemTriggerParams(
        WarriorStats stats = null,
        Vector2 gridIndex = default,
        Summoner summoner = null,
        Deck ownDeck = null,
        Deck enemyDeck = null,
        Hand enemyHand = null,
        Coin coin = null,
        GridManager gridManager = null
    ) {
        this.stats = stats;
        this.gridIndex = gridIndex;
        this.summoner = summoner;
        this.ownDeck = ownDeck;
        this.enemyDeck = enemyDeck;
        this.enemyHand = enemyHand;
        this.coin = coin;
        this.gridManager = gridManager;
    }
}