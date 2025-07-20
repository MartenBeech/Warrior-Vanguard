using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CharacterSpawner : MonoBehaviour {
    public enum Alignment {
        Null, Enemy, Friend
    };
    public GameObject warriorPrefab;
    public Alignment spawningAlignment;
    public GridManager gridManager;
    public Transform warriorsObject;
    public GameManager gameManager;
    public HoverCard hoverCard;
    public Hand friendHand;
    public Hand enemyHand;
    public Transform friendSummonerObject;
    public Transform enemySummonerObject;

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

        if (stats.alignment == Alignment.Enemy) {
            ItemManager.enemyItem.UseOnFriendSpawn(stats);
        } else if (stats.alignment == Alignment.Friend) {
            ItemManager.enemyItem.UseOnEnemySpawn(stats);
        }

        GameObject warriorObject = Instantiate(warriorPrefab, from, Quaternion.identity, warriorsObject);
        warriorObject.name = stats.displayTitle;
        warriorObject.GetComponent<RectTransform>().localScale = gridManager.GetCellDimension() / warriorObject.GetComponent<RectTransform>().rect.width;
        Character character = warriorObject.GetComponent<Character>();
        ObjectAnimation objectAnimation = warriorObject.GetComponentInChildren<ObjectAnimation>();

        List<Character> friends = gridManager.GetFriends(stats.alignment);
        foreach (Character friend in friends) {
            friend.stats.ability.boneSculptor.Trigger(friend, stats);
            friend.stats.ability.forestStrength.TriggerSummonFriend(friend, stats);
            friend.stats.ability.evilInspiration.TriggerSummonFriend(friend, stats);
            friend.stats.ability.forestProtection.TriggerSummonFriend(friend, stats);
            friend.stats.ability.massResistance.TriggerSummonFriend(friend, stats);
            friend.stats.ability.massEnflame.TriggerSummonFriend(friend, stats);
            friend.stats.ability.massImmolate.TriggerSummonFriend(friend, stats);
        }

        Hand hand = null;
        Transform summonerObject = null;
        if (stats.alignment == Alignment.Friend) {
            hand = friendHand;
            summonerObject = friendSummonerObject;
        } else if (stats.alignment == Alignment.Enemy) {
            hand = enemyHand;
            summonerObject = enemySummonerObject;
        }
        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        Summoner summoner = summonerObject.GetComponent<Summoner>();
        character.Initiate(gameManager, gridManager, hand, this, summonerObject, summoner, hoverCard, floatingText);

        character.gridIndex = gridIndex;
        character.SetStats(stats);
        character.stats.alignment = stats.alignment;

        stats.ability.skeletal.TriggerSummon(character, summoner);
        stats.ability.familiarGround.TriggerSummon(character);

        gameManager.RegisterCharacter(character, stats.alignment);
        await objectAnimation.MoveObject(from, gridManager.GetCellPosition(gridIndex));

        character.SetPosition(gridIndex);

        stats.ability.forestStrength.TriggerSummon(character, gridManager);
        stats.ability.evilInspiration.TriggerSummon(character, gridManager);
        stats.ability.forestProtection.TriggerSummon(character, gridManager);
        stats.ability.massResistance.TriggerSummon(character, gridManager);
        stats.ability.massEnflame.TriggerSummon(character, gridManager);
        stats.ability.massImmolate.TriggerSummon(character, gridManager);

        List<Character> enemies = gridManager.GetEnemies(stats.alignment);
        foreach (Character enemy in enemies) {
            await enemy.stats.ability.staticEntrance.Trigger(enemy, character);
        }

        foreach (Item item in ItemManager.LoadItems()) {
            if (stats.alignment == Alignment.Friend) {
                await item.UseAfterFriendSpawn(stats, gridIndex);
            } else if (stats.alignment == Alignment.Enemy) {
                await item.UseAfterEnemySpawn(stats, gridIndex);
            }
        }

        if (stats.alignment == Alignment.Enemy) {
            await ItemManager.enemyItem.UseAfterFriendSpawn(stats, gridIndex);
        } else if (stats.alignment == Alignment.Friend) {
            await ItemManager.enemyItem.UseAfterEnemySpawn(stats, gridIndex);
        }

        await stats.ability.spawn.Trigger(character, this);

    }

    public async Task SpawnRandomly(WarriorStats stats, Vector2 from) {
        GridCell randomCell = gridManager.GetRandomEmptyDeploy(stats.ability.construct.GetValue(stats), stats.alignment);

        if (!randomCell) return;

        await Spawn(randomCell.gridIndex, stats, from);
    }
}
