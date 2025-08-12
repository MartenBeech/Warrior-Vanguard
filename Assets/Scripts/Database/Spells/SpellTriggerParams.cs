public class SpellTriggerParams {
    public GridManager gridManager;
    public Character target;
    public int cardLevel;
    public FloatingText floatingText;
    public CharacterSpawner characterSpawner;
    public Deck deck;
    public Summoner summoner;
    public Hand hand;

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