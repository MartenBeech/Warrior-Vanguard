using UnityEngine;

public class CharacterSpawner : MonoBehaviour {
    public enum Alignment {
        Null, Enemy, Friend
    };
    public GameObject warriorPrefab;
    private Alignment spawningAlignment;
    public GridManager gridManager;
    public Transform warriorsObject;
    public GameManager gameManager;
    public HoverWarrior hoverWarrior;
    public Hand hand;
    public Transform EnemySummonerObject;

    public void ActivateSpawnEnemy() {
        spawningAlignment = Alignment.Enemy;
    }
    public void ActivateSpawn(Alignment alignment) {
        spawningAlignment = alignment;
    }

    public bool getIsSpawning(Alignment alignment) {
        return spawningAlignment == alignment;
    }

    public void SpawnCharacter(Vector2 cell) {
        if (spawningAlignment == Alignment.Null) return;

        Spawn(cell, warriorPrefab, spawningAlignment);
        spawningAlignment = Alignment.Null;
    }

    private async void Spawn(Vector2 cell, GameObject prefab, Alignment alignment) {
        Card selectedCard = hand.selectedCard;
        WarriorStats stats = alignment == Alignment.Friend ? selectedCard.stats : CardDatabase.Instance.allCards[1];

        Vector2 playedCardPos = alignment == Alignment.Friend ? selectedCard.GetComponent<RectTransform>().position : EnemySummonerObject.position;

        GameObject warriorAnimation = Instantiate(prefab, playedCardPos, Quaternion.identity, warriorsObject);
        Character characterAnimation = warriorAnimation.GetComponent<Character>();
        ObjectAnimation objectAnimation = warriorAnimation.GetComponentInChildren<ObjectAnimation>();
        characterAnimation.SetStats(stats);
        await objectAnimation.MoveObject(playedCardPos, cell);
        Destroy(warriorAnimation);

        Vector2 spawnPosition = cell;
        GameObject warrior = Instantiate(prefab, spawnPosition, Quaternion.identity, warriorsObject);
        warrior.GetComponent<RectTransform>().localScale = gridManager.getCellDimension() / warrior.GetComponent<RectTransform>().rect.width;
        Character character = warrior.GetComponent<Character>();
        character.Initiate(gameManager, gridManager);

        character.SetAlignment(alignment);

        if (alignment == Alignment.Friend) {
            character.SetHoverWarrior(hoverWarrior);
            character.SetStats(stats);
        } else {
            //Creates new instance of the card stats to avoid modifying the original card stats
            character.SetStats(new WarriorStats {
                attack = stats.attack,
                health = stats.health,
                title = stats.title,
                cost = stats.cost,
                movementSpeed = stats.movementSpeed,
                range = stats.range,
                numberOfAttacks = stats.numberOfAttacks
            });
        }

        character.SetPosition(cell);
        gameManager.RegisterCharacter(character, alignment);
    }

}
