using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    public Hand friendHand;
    public Hand enemyHand;
    public Transform friendSummonerObject;
    public Transform enemySummonerObject;

    public void ActivateSpawnEnemy() {
        spawningAlignment = Alignment.Enemy;
        gridManager.HighlightAllCells();
    }
    public void ActivateSpawn(Alignment alignment) {
        spawningAlignment = alignment;
    }

    public bool getIsSpawning(Alignment alignment) {
        return spawningAlignment == alignment;
    }

    public async Task Spawn(Vector2 gridIndex, WarriorStats stats, Vector2 from) {

        foreach (Item item in ItemManager.LoadItems()) {
            if (stats.alignment == Alignment.Friend) {
                item.UseOnFriendSpawn(stats);
            } else if (stats.alignment == Alignment.Enemy) {
                item.UseOnEnemySpawn(stats);
            }
        }

        GameObject warriorObject = Instantiate(warriorPrefab, from, Quaternion.identity, warriorsObject);
        warriorObject.name = stats.title;
        warriorObject.GetComponent<RectTransform>().localScale = gridManager.GetCellDimension() / warriorObject.GetComponent<RectTransform>().rect.width;
        Character character = warriorObject.GetComponent<Character>();
        ObjectAnimation objectAnimation = warriorObject.GetComponentInChildren<ObjectAnimation>();

        List<Character> friends = gridManager.GetFriends(stats.alignment);
        foreach (Character friend in friends) {
            friend.stats.ability.boneSculptor.Trigger(friend, stats);
        }

        character.SetStats(stats);
        character.gridIndex = gridIndex;

        stats.ability.skeletal.TriggerSummon(character, gameManager);

        gameManager.RegisterCharacter(character, stats.alignment);
        await objectAnimation.MoveObject(from, gridManager.GetCellPosition(gridIndex));


        Hand hand = null;
        Transform summonerObject = null;
        if (stats.alignment == Alignment.Friend) {
            hand = friendHand;
            summonerObject = friendSummonerObject;
        } else if (stats.alignment == Alignment.Enemy) {
            hand = enemyHand;
            summonerObject = enemySummonerObject;
        }
        character.Initiate(gameManager, gridManager, hand, this, summonerObject);

        character.SetAlignment(stats.alignment);
        character.SetHoverWarrior(hoverWarrior);

        character.SetPosition(gridIndex);

        foreach (Item item in ItemManager.LoadItems()) {
            if (stats.alignment == Alignment.Friend) {
                await item.UseAfterFriendSpawn(stats, gridIndex);
            } else if (stats.alignment == Alignment.Enemy) {
                await item.UseAfterEnemySpawn(stats, gridIndex);
            }
        }
    }

    public async Task SpawnRandomly(WarriorStats stats, Vector2 from) {
        GridCell randomCell = gridManager.GetRandomEmptyDeploy(stats.ability.construct.Trigger(stats), stats.alignment);
        if (!randomCell) return;

        await Spawn(randomCell.gridIndex, stats, from);
    }
}
