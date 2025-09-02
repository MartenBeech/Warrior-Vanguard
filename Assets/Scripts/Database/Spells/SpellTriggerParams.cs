public class SpellTriggerParams {
    public GridManager gridManager;
    public Warrior target;
    public int cardLevel;
    public FloatingText floatingText;
    public WarriorSummoner warriorSummoner;
    public Deck deck;
    public Summoner summoner;
    public Hand hand;

    public SpellTriggerParams(
        GridManager gridManager = null,
        Warrior target = null,
        int cardLevel = 0,
        FloatingText floatingText = null,
        WarriorSummoner warriorSummoner = null,
        Deck deck = null,
        Summoner summoner = null,
        Hand hand = null
    ) {
        this.gridManager = gridManager;
        this.target = target;
        this.cardLevel = cardLevel;
        this.floatingText = floatingText;
        this.warriorSummoner = warriorSummoner;
        this.deck = deck;
        this.summoner = summoner;
        this.hand = hand;
    }
}