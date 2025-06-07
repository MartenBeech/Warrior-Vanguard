using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Collections.Generic;

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
    public GameObject crystal;
    private GameManager gameManager;
    public enum DamageType {
        Physical, Magical
    };
    public enum Race {
        None, Construct, Ghoul, Lich, Skeleton, Vampire, Wraith, Zombie, Human, Dark, Unicorn, Elf, Dwarf, Centaur, Dragon, Troll, Treant, Imp, Minotaur, Pirate, Holyborn, Werewolf, Pixie
    }
    private Hand hand;
    private CharacterSpawner characterSpawner;
    private Transform summonerObject;
    private Summoner summoner;
    private FloatingText floatingText;

    public void Initiate(GameManager gameManager, GridManager gridManager, Hand hand, CharacterSpawner characterSpawner, Transform summonerObject, HoverWarrior hoverWarrior, FloatingText floatingText) {
        this.gameManager = gameManager;
        this.gridManager = gridManager;
        this.hand = hand;
        this.characterSpawner = characterSpawner;
        this.summonerObject = summonerObject;
        summoner = summonerObject.GetComponent<Summoner>();
        this.hoverWarrior = hoverWarrior;
        this.floatingText = floatingText;
    }

    public void UpdateWarriorUI() {
        if (this == null) return;

        attackText.text = $"{stats.GetStrength()}";
        healthText.text = $"{stats.GetHealth()}";

        Sprite sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");
        image.GetComponent<Image>().sprite = sprite != null ? sprite : Resources.Load<Sprite>($"Images/Icons/Red Cross");

        if (alignment == CharacterSpawner.Alignment.Friend) {
            crystal.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.green);
        } else if (alignment == CharacterSpawner.Alignment.Enemy) {
            crystal.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.red);
        }

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
                if (!stats.ability.flying.GetValue(stats) || frontCellCharacter.stats.ability.flying.GetValue(frontCellCharacter.stats))
                    break;
            }
        }

        if (stepsToMove > 0) {
            if (stats.ability.rooted.Trigger(this)) return;

            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, stepsToMove);
            ObjectAnimation objectAnimation = GetComponent<ObjectAnimation>();
            await objectAnimation.MoveObject(transform.position, gridManager.GetCellPosition(newGridIndex), 2);
            stats.ability.familiarGround.TriggerMove(this, gridIndex, newGridIndex, gridManager);
            gridIndex = newGridIndex;
        }
    }

    public bool IsOutOfField(Vector2 gridIndex) {
        return gridIndex.x < 0 || gridIndex.x >= gridManager.columns;
    }

    public async Task StandAndAttack(Direction direction) {
        if (stats.GetHealth() <= 0) return;
        if (stats.ability.stunned.Trigger(this)) return;

        for (int i = 1; i <= stats.range; i++) {
            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, i);
            Character characterOnCell = gridManager.GetCellCharacter(newGridIndex);

            if (characterOnCell && characterOnCell.alignment != alignment) {
                await Attack(characterOnCell);
                break;
            }

            if (IsOutOfField(newGridIndex)) {
                Summoner summonerTarget = alignment == CharacterSpawner.Alignment.Enemy ?
                    gameManager.friendSummonerObject.GetComponent<Summoner>() :
                    gameManager.enemySummonerObject.GetComponent<Summoner>();

                await summonerTarget.Damage(this, stats.GetStrength(), gridManager);
                break;
            }
        }
    }

    public async Task Attack(Character target) {
        for (int i = 0; i < (stats.ability.doubleStrike.GetValue(stats) ? 2 : 1); i++) {
            if (target.stats.GetHealth() > 0) {
                List<Task> asyncFunctions = new() {
                stats.ability.multishot.Trigger(this, target, gridManager),
                stats.ability.splash.Trigger(this, target, gridManager),
                stats.ability.pierce.Trigger(this, target, gridManager),
                Strike(target)
            };
                await Task.WhenAll(asyncFunctions);
                await target.stats.ability.spikes.Trigger(this, target);
            }
        }

        if (target.stats.GetHealth() > 0) {
            stats.ability.weaken.Trigger(this, target);
        }
        target.stats.ability.weakeningAura.Trigger(this, target);
        target.stats.ability.poisoningAura.Trigger(this, target);
        stats.ability.bloodlust.Trigger(this);


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

    public async Task Strike(Character target) {
        int damage = stats.GetStrength();

        damage = stats.ability.stealth.TriggerStrike(this, damage);

        stats.ability.poison.Trigger(this, target);
        stats.ability.frozenTouch.Trigger(this, target);

        damage = await target.TakeDamage(this, damage, stats.damageType);

        if (damage > 0) {
            await stats.ability.lifeSteal.Trigger(this, damage);
            await stats.ability.lifeTransfer.Trigger(this, damage, gridManager);
        }
        await stats.ability.bash.Trigger(this, target, floatingText);
        stats.ability.rooting.Trigger(this, target);
    }

    public async Task<int> TakeDamage(Character dealer, int damage, DamageType damageType) {
        damage = stats.ability.sapPower.Trigger(dealer, this, damage);
        damage = stats.ability.armor.Trigger(this, damage, damageType);
        damage = stats.ability.resistance.Trigger(this, damage, damageType);
        damage = stats.ability.stealth.TriggerTakeDamage(this, damage);
        damage = stats.ability.thickSkin.Trigger(this, damage);

        damage = stats.ability.stoneskin.Trigger(this, damage);
        damage = stats.ability.incorporeal.Trigger(this, damage, damageType);

        List<Task> asyncFunctions = new();

        if (damage > 0) {
            stats.AddHealthCurrent(-damage);
            UpdateWarriorUI();

            if (stats.GetHealth() <= 0) {
                asyncFunctions.Add(Die(dealer));
            } else {
                stats.ability.vengeance.Trigger(this);
            }
        }

        dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.red);

        asyncFunctions.Add(floatingText.CreateFloatingText(transform, damage.ToString(), ColorPalette.ColorEnum.red, true));

        await Task.WhenAll(asyncFunctions);

        if (dealer) {
            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
        }

        return damage;
    }

    public async Task Heal(Character dealer, int amount) {
        if (!this || !dealer) return;

        if (stats.GetHealth() < stats.GetHealthMax()) {
            stats.AddHealthCurrent(amount);
            UpdateWarriorUI();

            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.green);

            await floatingText.CreateFloatingText(transform, amount.ToString(), ColorPalette.ColorEnum.green, true);

            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.white);
        }
    }

    public async Task Die(Character dealer) {
        gameManager.RemoveCharacter(this);
        gridManager.RemoveCharacter(this);

        stats.ability.skeletal.TriggerDeath(this, gameManager);
        stats.ability.forestStrength.TriggerDeath(this, gridManager);
        stats.ability.evilInspiration.TriggerDeath(this, gridManager);
        stats.ability.forestProtection.TriggerDeath(this, gridManager);

        List<Task> asyncFunctions = new() {
            stats.ability.revive.Trigger(this, characterSpawner),
            stats.ability.hydraSplit.Trigger(this, characterSpawner),
            stats.ability.boneSpread.Trigger(this, characterSpawner),
            stats.ability.phoenixAshes.Trigger(this, characterSpawner),
        };

        if (stats.ability.afterlife.GetValue(stats)) {
            GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity, transform.parent);
            asyncFunctions.Add(stats.ability.afterlife.Trigger(this, gridManager, hand, summonerObject, clone));
        }

        if (dealer != this) {
            dealer.stats.ability.cannibalism.Trigger(dealer);
            asyncFunctions.Add(dealer.stats.ability.carnivore.Trigger(dealer, this));
            asyncFunctions.Add(dealer.stats.ability.raiseDead.Trigger(dealer, this, characterSpawner));

            List<Character> friends = gridManager.GetFriends(dealer.alignment);
            foreach (Character friend in friends) {
                asyncFunctions.Add(friend.stats.ability.deathCall.Trigger(friend, this, characterSpawner));
            }

            List<Character> characters = gridManager.GetCharacters();
            foreach (Character character in characters) {
                asyncFunctions.Add(character.stats.ability.looting.Trigger(character, floatingText));
            }

            asyncFunctions.Add(dealer.stats.ability.possess.Trigger(dealer, this, characterSpawner));

            asyncFunctions.Add(dealer.stats.ability.greedyStrike.Trigger(dealer, floatingText));
        }

        gameObject.SetActive(false);

        if (alignment == CharacterSpawner.Alignment.Friend) {
            foreach (Item item in ItemManager.LoadItems()) {
                await item.UseOnWarriorDeath(summoner);
            }
        }

        Destroy(gameObject);
        await Task.WhenAll(asyncFunctions);
    }

    public async Task EndTurn() {
        stats.ability.poisonCloud.Trigger(this, gridManager);
        await stats.ability.poisoned.Trigger(this);
        await stats.ability.cemeteryGates.Trigger(this, characterSpawner);
        await stats.ability.rebirth.Trigger(this, characterSpawner);
        await stats.ability.regeneration.Trigger(this);
        await stats.ability.sprout.Trigger(this, characterSpawner);
        await stats.ability.sapEnergy.Trigger(this, gridManager);
        await stats.ability.massHeal.Trigger(this, gridManager);
        await stats.ability.lushGrounds.Trigger(this, gridManager);
        await stats.ability.heal.Trigger(this, gridManager);
        await stats.ability.faeMagic.Trigger(this, summoner);
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
