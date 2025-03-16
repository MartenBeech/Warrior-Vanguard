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

    public void ActivateSpawnEnemy() {
        spawningAlignment = Alignment.Enemy;
    }
    public void ActivateSpawn(Alignment alignment) {
        spawningAlignment = alignment;
    }

    public bool getIsSpawning(Alignment alignment) {
        return spawningAlignment == alignment;
    }

    public async void Spawn(Vector2 gridIndex, WarriorStats stats, Alignment alignment, Vector2 from) {

        GameObject warrior = Instantiate(warriorPrefab, from, Quaternion.identity, warriorsObject);
        Character character = warrior.GetComponent<Character>();
        ObjectAnimation objectAnimation = warrior.GetComponentInChildren<ObjectAnimation>();
        character.SetStats(stats);
        character.gridIndex = gridIndex;
        gameManager.RegisterCharacter(character, alignment);
        await objectAnimation.MoveObject(from, gridManager.GetCellPosition(gridIndex));

        warrior.GetComponent<RectTransform>().localScale = gridManager.GetCellDimension() / warrior.GetComponent<RectTransform>().rect.width;
        character.Initiate(gameManager, gridManager);

        character.SetAlignment(alignment);
        character.SetHoverWarrior(hoverWarrior);

        character.SetPosition(gridIndex);
    }
}
