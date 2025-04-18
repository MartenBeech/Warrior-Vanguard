using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public Vector2 gridIndex;
    private GridManager gridManager;
    public WarriorStats stats;
    private HoverWarrior hoverWarrior;
    public enum Direction {
        Left, Right
    };
    public CharacterSpawner.Alignment alignment;

    public TMP_Text attackText;
    public TMP_Text healthText;
    public GameObject image;
    private GameManager gameManager;
    public enum DamageType {
        Physical, Magical
    };
    private Hand hand;
    private CharacterSpawner characterSpawner;
    private Transform summonerObject;

    public void Initiate(GameManager gameManager, GridManager gridManager, Hand hand, CharacterSpawner characterSpawner, Transform summonerObject) {
        this.gameManager = gameManager;
        this.gridManager = gridManager;
        this.hand = hand;
        this.characterSpawner = characterSpawner;
        this.summonerObject = summonerObject;
    }

    public void UpdateWarriorUI() {
        attackText.text = $"{stats.GetStrength()}";
        healthText.text = $"{stats.GetHealth()}";
        string cleanTitle = stats.title.Replace("+", string.Empty);
        cleanTitle = Regex.Replace(cleanTitle, "(?<!^)([A-Z])", " $1");
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Cards/{cleanTitle}");

        if (stats.ability.stealth.GetValue(stats)) {
            image.GetComponent<Image>().color = ColorPalette.AddTransparency(image.GetComponent<Image>().color, 70);
        } else {
            image.GetComponent<Image>().color = ColorPalette.AddTransparency(image.GetComponent<Image>().color, 100);
        }
    }

    public void SetStats(WarriorStats warriorStats) {
        stats = warriorStats;
        UpdateWarriorUI();
    }

    public void SetAlignment(CharacterSpawner.Alignment alignment) {
        this.alignment = alignment;
    }

    public void SetHoverWarrior(HoverWarrior hoverWarrior) {
        this.hoverWarrior = hoverWarrior;
    }

    public void SetPosition(Vector2 position) {
        gridIndex = position;
        transform.position = gridManager.GetCellPosition(position);
    }

    public async Task MoveWarrior(Direction direction) {
        if (stats.GetHealth() <= 0) return;

        int stepsToMove = 0;
        for (int i = 1; i <= stats.speed; i++) {
            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, i);

            if (IsOutOfField(newGridIndex)) break;

            Character frontCellCharacter = gridManager.GetCellCharacter(newGridIndex);
            if (!frontCellCharacter) {
                stepsToMove = i;
            } else if (frontCellCharacter && frontCellCharacter.alignment != alignment) {
                break;
            }
        }

        if (stepsToMove > 0) {
            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, stepsToMove);
            ObjectAnimation objectAnimation = GetComponent<ObjectAnimation>();
            await objectAnimation.MoveObject(transform.position, gridManager.GetCellPosition(newGridIndex), 2);
            gridIndex = newGridIndex;
        }
    }

    public bool IsOutOfField(Vector2 gridIndex) {
        return gridIndex.x < 0 || gridIndex.x >= gridManager.columns;
    }

    public async Task StandAndAttack(Direction direction) {
        if (stats.GetHealth() <= 0) return;

        for (int i = 1; i <= stats.range; i++) {
            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, i);
            Character characterOnCell = gridManager.GetCellCharacter(newGridIndex);

            if (characterOnCell && characterOnCell.alignment != alignment) {
                await Attack(characterOnCell);
                break;
            }

            if (IsOutOfField(newGridIndex)) {
                Summoner summoner = alignment == CharacterSpawner.Alignment.Enemy ?
                    gameManager.friendSummonerObject.GetComponent<Summoner>() :
                    gameManager.enemySummonerObject.GetComponent<Summoner>();

                await summoner.Damage(this, stats.GetStrength(), gridManager);
                break;
            }
        }
    }

    public async Task Attack(Character target) {
        int damage = stats.GetStrength();

        damage = stats.ability.stealth.TriggerAttack(this, damage);

        List<Task> asyncFunctions = new() {
            stats.ability.splash.Trigger(this, target, gridManager),
            Strike(target, damage)
        };

        if (target.stats.GetHealth() > 0) {
            stats.ability.weaken.Trigger(this, target);
        }
        target.stats.ability.weakeningAura.Trigger(this, target);
        target.stats.ability.poisoningAura.Trigger(this, target);
        stats.ability.bloodlust.Trigger(this);

        await Task.WhenAll(asyncFunctions);

        if (target.stats.GetHealth() > 0) {
            if (stats.ability.darkTouch.Trigger(this, target)) {
                await target.Die(this);
            }
        }

        if (target.stats.GetHealth() > 0) {
            await target.stats.ability.retaliate.Trigger(this, target, gridManager);
        }

        await stats.ability.hitAndRun.Trigger(this);
    }

    public async Task Strike(Character target, int damage) {
        stats.ability.poison.Trigger(this, target);
        stats.ability.frozenTouch.Trigger(this, target);

        damage = await target.TakeDamage(this, damage, stats.damageType);

        if (damage > 0) {
            await stats.ability.lifeSteal.Trigger(this, damage);
            await stats.ability.lifeTransfer.Trigger(this, damage, gridManager);
        }
    }

    public async Task<int> TakeDamage(Character dealer, int damage, DamageType damageType) {
        damage = stats.ability.stealth.TriggerTakeDamage(this, damage);
        damage = stats.ability.armor.Trigger(this, damage, damageType);

        damage = stats.ability.incorporeal.Trigger(this, damage, damageType);

        List<Task> asyncFunctions = new();

        if (damage > 0) {
            stats.AddHealthCurrent(-damage);
            UpdateWarriorUI();

            if (stats.GetHealth() <= 0) {
                asyncFunctions.Add(Die(dealer));
            }
        }

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        asyncFunctions.Add(floatingText.CreateFloatingText(transform, damage.ToString(), ColorPalette.ColorEnum.red, true));

        Color currentColor = dealer.image.GetComponent<Image>().color;
        dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.red);

        await Task.WhenAll(asyncFunctions);
        if (dealer) {
            dealer.image.GetComponent<Image>().color = currentColor;
        }

        return damage;
    }

    public async Task Heal(Character dealer, int amount) {
        stats.AddHealthCurrent(amount);
        UpdateWarriorUI();

        Color currentColor = dealer.image.GetComponent<Image>().color;
        dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.green);

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(transform, amount.ToString(), ColorPalette.ColorEnum.green, true);

        dealer.image.GetComponent<Image>().color = currentColor;
    }

    public async Task Die(Character dealer) {
        gameManager.RemoveCharacter(this);
        gridManager.RemoveCharacter(this);

        List<Task> asyncFunctions = new() {
            stats.ability.revive.Trigger(this, characterSpawner),
            stats.ability.hydraSplit.Trigger(this, characterSpawner),
            stats.ability.boneSpread.Trigger(this, characterSpawner),
        };
        if (stats.ability.afterlife.GetValue(stats)) {
            GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity, transform.parent);
            asyncFunctions.Add(stats.ability.afterlife.Trigger(this, gridManager, hand, summonerObject, clone));
        }
        stats.ability.skeletal.TriggerDeath(this, gameManager);

        if (dealer != this) {
            dealer.stats.ability.cannibalism.Trigger(dealer);
            asyncFunctions.Add(dealer.stats.ability.raiseDead.Trigger(dealer, this, characterSpawner));
            List<Character> friends = gridManager.GetFriends(dealer.alignment);
            foreach (Character friend in friends) {
                asyncFunctions.Add(friend.stats.ability.deathCall.Trigger(friend, this, characterSpawner));
            }
            asyncFunctions.Add(dealer.stats.ability.possess.Trigger(dealer, this, characterSpawner));
        }

        gameObject.SetActive(false);

        if (alignment == CharacterSpawner.Alignment.Friend) {
            foreach (Item item in ItemManager.LoadItems()) {
                await item.UseOnWarriorDeath(summonerObject.GetComponent<Summoner>());
            }
        }

        Destroy(gameObject);
        await Task.WhenAll(asyncFunctions);
    }

    public async Task EndTurn() {
        stats.ability.poisonCloud.Trigger(this, gridManager);

        List<Task> asyncFunctions = new() {
            stats.ability.poisoned.Trigger(this),
            stats.ability.cemeteryGates.Trigger(this, characterSpawner)
        };
        await Task.WhenAll(asyncFunctions);
    }

    private Vector2 GetFrontCellIndex(Vector2 gridIndex, Direction direction, int range = 1) {
        if (direction == Direction.Left) {
            return new(gridIndex.x - (1 * range), gridIndex.y);
        } else if (direction == Direction.Right) {
            return new(gridIndex.x + (1 * range), gridIndex.y);
        }
        return gridIndex;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.ShowCardFromBattlefield(stats, gridManager.GetCellPosition(gridIndex));
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverWarrior) {
            hoverWarrior.HideCard();
        }
    }

    public async void OnClick() {
        await gridManager.SelectCell(gridIndex);
    }
}
