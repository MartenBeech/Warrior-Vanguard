using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class WarriorSummoner : MonoBehaviour {
    public enum Alignment {
        Null, Enemy, Friend
    };
    public GameObject warriorPrefab;
    public Alignment summoningAlignment;
    public GridManager gridManager;
    public Transform warriorsObject;
    public GameManager gameManager;
    public HoverCard hoverCard;
    public Hand friendHand;
    public Hand enemyHand;
    public Transform friendSummonerObject;
    public Transform enemySummonerObject;

    public void ActivateSummon(Alignment alignment) {
        summoningAlignment = alignment;
    }

    public bool getIsSummoning(Alignment alignment) {
        return summoningAlignment == alignment;
    }

    public async Task Summon(Vector2 gridIndex, WarriorStats stats, Vector2 from) {
        foreach (Item item in ItemManager.items) {
            item.UseOnWarriorSummon(new(stats));
            if (stats.alignment == Alignment.Friend) {
                item.UseOnFriendSummon(new(stats));
            } else if (stats.alignment == Alignment.Enemy) {
                item.UseOnEnemySummon(new(stats));
            }
        }

        ItemManager.enemyItem.UseOnWarriorSummon(new(stats));
        if (stats.alignment == Alignment.Enemy) {
            ItemManager.enemyItem.UseOnFriendSummon(new(stats));
        } else if (stats.alignment == Alignment.Friend) {
            ItemManager.enemyItem.UseOnEnemySummon(new(stats));
        }

        GameObject warriorObject = Instantiate(warriorPrefab, from, Quaternion.identity, warriorsObject);
        warriorObject.name = stats.displayTitle;
        warriorObject.GetComponent<RectTransform>().localScale = gridManager.GetCellDimension() / warriorObject.GetComponent<RectTransform>().rect.width;
        Warrior warrior = warriorObject.GetComponent<Warrior>();
        ObjectAnimation objectAnimation = warriorObject.GetComponentInChildren<ObjectAnimation>();

        List<Warrior> friends = gridManager.GetFriends(stats.alignment);
        foreach (Warrior friend in friends) {
            friend.stats.ability.boneSculptor.TriggerSummonFriend(friend, stats);
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
        warrior.Initiate(gameManager, gridManager, hand, this, summonerObject, summoner, hoverCard, floatingText);

        warrior.gridIndex = gridIndex;
        warrior.SetStats(stats);
        warrior.stats.alignment = stats.alignment;

        stats.ability.skeletal.TriggerSummon(warrior, summoner);
        stats.ability.familiarGround.TriggerSummon(warrior);

        gameManager.RegisterWarrior(warrior, stats.alignment);
        await objectAnimation.MoveObject(from, gridManager.GetCellPosition(gridIndex));

        warrior.SetPosition(gridIndex);

        stats.ability.forestStrength.TriggerSummon(warrior, gridManager);
        stats.ability.evilInspiration.TriggerSummon(warrior, gridManager);
        stats.ability.forestProtection.TriggerSummon(warrior, gridManager);
        stats.ability.massResistance.TriggerSummon(warrior, gridManager);
        stats.ability.massEnflame.TriggerSummon(warrior, gridManager);
        stats.ability.massSelfHarm.TriggerSummon(warrior, gridManager);
        stats.ability.massImmolate.TriggerSummon(warrior, gridManager);
        stats.ability.summoningSpirits.TriggerSummon(warrior, gridManager);
        await stats.ability.drawing.TriggerSummon(warrior, gameManager);
        stats.ability.familyDiscount.TriggerSummon(warrior, gridManager);
        stats.ability.raceDiscount.TriggerSummon(warrior, gridManager);

        List<Warrior> enemies = gridManager.GetEnemies(stats.alignment);
        foreach (Warrior enemy in enemies) {
            await enemy.stats.ability.staticEntrance.TriggerSummonEnemy(enemy, warrior);
            enemy.stats.ability.massSelfHarm.TriggerSummonEnemy(enemy, stats);
        }

        foreach (Item item in ItemManager.items) {
            if (stats.alignment == Alignment.Friend) {
                await item.UseAfterFriendSummon(new(stats, gridIndex, coin: gameManager.friendCoin, ownDeck: gameManager.friendDeck));
            } else if (stats.alignment == Alignment.Enemy) {
                await item.UseAfterEnemySummon(new(stats, gridIndex));
            }
        }

        if (stats.alignment == Alignment.Enemy) {
            await ItemManager.enemyItem.UseAfterFriendSummon(new(stats, gridIndex, coin: gameManager.enemyCoin, ownDeck: gameManager.enemyDeck));
        } else if (stats.alignment == Alignment.Friend) {
            await ItemManager.enemyItem.UseAfterEnemySummon(new(stats, gridIndex));
        }

        await stats.ability.spawn.TriggerSummon(warrior, this);
        await stats.ability.silence.TriggerSummon(warrior, gridManager, floatingText);
        await stats.ability.massSilence.TriggerSummon(warrior, gridManager, floatingText);
    }

    public async Task SummonRandomly(WarriorStats stats, Vector2 from) {
        GridCell randomCell = gridManager.GetRandomEmptyDeploy(stats.ability.construct.GetValue(stats), stats.alignment);
        stats.displayTitle = Regex.Replace(stats.title, "(?<!^)([A-Z])", " $1");

        if (!randomCell) return;

        await Summon(randomCell.gridIndex, stats, from);
    }
}
