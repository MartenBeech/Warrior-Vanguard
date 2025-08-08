public class SpellTriggerParams {
    public GridManager gridManager { get; set; }
    public Character target { get; set; }
    public int cardLevel { get; set; }
    public FloatingText floatingText { get; set; }
    public CharacterSpawner characterSpawner { get; set; }
    public Deck deck { get; set; }
    public Summoner summoner { get; set; }
    public Hand hand { get; set; }

    public SpellTriggerParams(
        GridManager gridManager = null,
        Character target = null,
        int cardLevel = 0,
        FloatingText floatingText = null,
        CharacterSpawner characterSpawner = null,
        Deck deck = null,
        Summoner summoner = null,
        Hand hand = null
    ) {
        this.gridManager = gridManager;
        this.target = target;
        this.cardLevel = cardLevel;
        this.floatingText = floatingText;
        this.characterSpawner = characterSpawner;
        this.deck = deck;
        this.summoner = summoner;
        this.hand = hand;
    }
}