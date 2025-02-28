using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public enum Alignment
    {
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

    public void ActivateSpawnEnemy()
    {
        spawningAlignment = Alignment.Enemy;
    }
    public void ActivateSpawn(Alignment alignment)
    {
        spawningAlignment = alignment;
    }

    public bool getIsSpawning(Alignment alignment)
    {
        return spawningAlignment == alignment;
    }

    public void SpawnCharacter(Vector2 cell)
    {
        if (spawningAlignment == Alignment.Null) return;

        Spawn(cell, warriorPrefab, spawningAlignment);
        spawningAlignment = Alignment.Null;
    }

    private async void Spawn(Vector2 cell, GameObject prefab, Alignment alignment)
    {
        Card selectedCard = hand.selectedCard;
        CardStats stats = alignment == Alignment.Friend ? selectedCard.stats : CardDatabase.Instance.allCards[1];

        Vector2 playedCardPos = alignment == Alignment.Friend ? selectedCard.GetComponent<RectTransform>().position : EnemySummonerObject.position;
        CardAnimation cardAnimation = prefab.GetComponentInChildren<CardAnimation>();

        GameObject warriorAnimation = Instantiate(prefab, playedCardPos, Quaternion.identity, warriorsObject);
        Character characterAnimation = warriorAnimation.GetComponent<Character>();
        characterAnimation.SetStats(stats);
        await cardAnimation.MoveCard(warriorAnimation, playedCardPos, cell);
        Destroy(warriorAnimation);

        Vector2 spawnPosition = cell;
        GameObject warrior = Instantiate(prefab, spawnPosition, Quaternion.identity, warriorsObject);
        warrior.GetComponent<RectTransform>().localScale = gridManager.getCellDimension() / warrior.GetComponent<RectTransform>().rect.width;
        Character character = warrior.GetComponent<Character>();

        character.SetAlignment(alignment);

        if (alignment == Alignment.Friend)
        {
            character.SetHoverWarrior(hoverWarrior);
            character.SetStats(stats);
        }
        else
        {
            character.SetStats(stats);
        }

        character.SetPosition(cell);
        character.SetGridManager(gridManager);
        gameManager.RegisterCharacter(character, alignment);
    }

}
