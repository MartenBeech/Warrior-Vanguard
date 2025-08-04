using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Collections.Generic;

public class Character : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public enum Race {
        None, Construct, Dragon, //Common
        Ghoul, Lich, Skeleton, Vampire, Wraith, Zombie, Nightrider, //Undead
        Human, Pirate, Holyborn, Knight, Griffin, Sorcerer, Fencer, Librarian, //Human
        Unicorn, Elf, Dwarf, Centaur, Troll, Treant, Werewolf, Pixie, //Forest
        Imp, Minotaur, Harpy, Pestilence, Cerberus, Succubus, Demon, //Underworld
        Dark, Fire, Light, Nature //Spells
    }
    public enum Genre {
        Human, Forest, Undead, Underworld, None
    }
    public Vector2 gridIndex;
    private GridManager gridManager;
    public WarriorStats stats;
    private HoverCard hoverCard;
    public enum Direction {
        Left, Right
    };
    public TMP_Text attackText;
    public TMP_Text healthText;
    public GameObject image;
    public GameObject crystal;
    private GameManager gameManager;
    public enum DamageType {
        Physical, Magical
    };

    private Hand hand;
    public CharacterSpawner characterSpawner;
    private Transform summonerObject;
    private Summoner summoner;
    private FloatingText floatingText;

    public void Initiate(GameManager gameManager, GridManager gridManager, Hand hand, CharacterSpawner characterSpawner, Transform summonerObject, Summoner summoner, HoverCard hoverCard, FloatingText floatingText) {
        this.gameManager = gameManager;
        this.gridManager = gridManager;
        this.hand = hand;
        this.characterSpawner = characterSpawner;
        this.summonerObject = summonerObject;
        this.summoner = summoner;
        this.hoverCard = hoverCard;
        this.floatingText = floatingText;
    }

    public void UpdateWarriorUI() {
        if (this == null) return;

        attackText.text = $"{stats.GetStrength()}";
        healthText.text = $"{stats.GetHealth()}";

        Sprite sprite = Resources.Load<Sprite>($"Images/Cards/{stats.title}");
        image.GetComponent<Image>().sprite = sprite != null ? sprite : Resources.Load<Sprite>($"Images/Icons/Red Cross");

        if (stats.alignment == CharacterSpawner.Alignment.Friend) {
            crystal.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.Green);
        } else if (stats.alignment == CharacterSpawner.Alignment.Enemy) {
            crystal.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.Red);
        }

        if (stats.ability.stealth.GetValue(stats)) {
            image.GetComponent<Image>().color = ColorPalette.AddTransparency(image.GetComponent<Image>().color, 60);
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
        if (stats.ability.stunned.GetValue(stats)) return;

        if (stats.ability.seduced.GetValue(stats)) {
            stats.alignment = stats.alignment == CharacterSpawner.Alignment.Enemy ? CharacterSpawner.Alignment.Friend : CharacterSpawner.Alignment.Enemy;
            direction = direction == Direction.Left ? Direction.Right : Direction.Left;
        }

        if (stats.ability.backstab.GetEnemyBehind(this, gridManager)) return;
        if (stats.ability.guard.GetRandomNearbyEnemy(this, gridManager)) return;

        int stepsToMove = 0;
        for (int i = 1; i <= stats.speed; i++) {
            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, i);

            if (IsOutOfField(newGridIndex)) break;

            Character frontCellCharacter = gridManager.GetCellCharacter(newGridIndex);
            if (!frontCellCharacter) {
                stepsToMove = i;
            } else if (frontCellCharacter && frontCellCharacter.stats.alignment != stats.alignment) {
                if (!stats.ability.flying.GetValue(stats) || frontCellCharacter.stats.ability.flying.GetValue(frontCellCharacter.stats))
                    break;
            }
        }

        if (stepsToMove > 0) {
            if (stats.ability.rooted.Trigger(this)) return;
            stats.ability.joust.Trigger(this, stepsToMove);

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
        if (stats.ability.stunned.GetValue(stats)) return;

        if (stats.ability.seduced.GetValue(stats)) {
            direction = direction == Direction.Left ? Direction.Right : Direction.Left;
        }

        if (await stats.ability.backstab.Trigger(this, gridManager)) return;
        if (await stats.ability.guard.Trigger(this, gridManager)) return;

        for (int i = 1; i <= stats.range; i++) {
            Vector2 newGridIndex = GetFrontCellIndex(gridIndex, direction, i);
            Character characterOnCell = gridManager.GetCellCharacter(newGridIndex);

            if (characterOnCell && characterOnCell.stats.alignment != stats.alignment) {
                await Attack(characterOnCell);
                break;
            }

            if (IsOutOfField(newGridIndex)) {
                await AttackSummoner();
                break;
            }
        }
    }

    public async Task Attack(Character target, bool dealDoubleDamage = false) {
        int damage = stats.GetStrength() + stats.tempStrength;
        if (dealDoubleDamage) {
            damage *= 2;
        }

        for (int i = 0; i < (stats.ability.doubleStrike.GetValue(stats) ? 2 : 1); i++) {
            if (target.stats.GetHealth() > 0) {
                await target.stats.ability.firstStrike.TriggerAttacked(this, target, gridManager);
                if (stats.GetHealth() < 0) return;

                List<Task> asyncFunctions = new() {
                stats.ability.multishot.TriggerAttack(this, target, gridManager),
                stats.ability.splash.TriggerAttack(this, target, gridManager),
                stats.ability.cleave.TriggerAttack(this, target, gridManager),
                stats.ability.pierce.TriggerAttack(this, target, gridManager),
                stats.ability.selfHarm.TriggerAttack(this),
                Strike(target, damage)
            };
                stats.ability.bloodlust.TriggerAttack(this);
                await Task.WhenAll(asyncFunctions);

                if (target.stats.GetHealth() > 0) {
                    target.stats.ability.firewall.TriggerAttacked(this, target);
                    target.stats.ability.weakeningAura.TriggerAttacked(this, target);
                    target.stats.ability.poisoningAura.TriggerAttacked(this, target);
                    if (stats.ability.darkTouch.TriggerAttack(this, target)) {
                        await target.Die(this);
                        return;
                    }
                    await target.stats.ability.spikes.TriggerAttacked(this, target);
                    await target.stats.ability.retaliate.TriggerAttacked(this, target, gridManager);
                }
            }
        }

        stats.tempStrength = 0;
    }

    public async Task AttackSummoner() {
        Summoner summonerTarget = null;
        if (stats.alignment == CharacterSpawner.Alignment.Friend) {
            summonerTarget = gameManager.enemySummonerObject.GetComponent<Summoner>(); ;
        } else if (stats.alignment == CharacterSpawner.Alignment.Enemy) {
            summonerTarget = gameManager.friendSummonerObject.GetComponent<Summoner>();
        }

        Deck deck = null;
        if (stats.alignment == CharacterSpawner.Alignment.Friend) {
            deck = gameManager.friendDeck;
        } else if (stats.alignment == CharacterSpawner.Alignment.Enemy) {
            deck = gameManager.enemyDeck;
        }

        for (int nAttacks = 0; nAttacks < (stats.ability.doubleStrike.GetValue(stats) ? 2 : 1); nAttacks++) {
            await summonerTarget.TakeDamage(this, stats.GetStrength(), gridManager, stats.damageType);
            await stats.ability.soulSiphon.TriggerAttack(this, deck);
        }
    }

    public async Task Strike(Character target, int damage = -1) {
        if (damage == -1) {
            damage = stats.GetStrength();
        }

        damage = stats.ability.stealth.TriggerStrike(this, damage);
        damage = stats.ability.enflame.TriggerStrike(this, target, damage);

        stats.ability.poison.TriggerStrike(this, target);
        stats.ability.frozenTouch.TriggerStrike(this, target);
        stats.ability.weaken.TriggerStrike(this, target);
        stats.ability.bleed.TriggerStrike(this, target);

        damage = await target.TakeDamage(this, damage, stats.damageType);

        stats.ability.vulnerability.TriggerStrike(this, target);

        if (damage > 0) {
            await stats.ability.lifeSteal.TriggerStrike(this, damage);
            await stats.ability.lifeTransfer.TriggerStrike(this, damage, gridManager);
        }
        await stats.ability.bash.TriggerStrike(this, target, floatingText);
        await stats.ability.seduce.TriggerStrike(this, target, floatingText);
        stats.ability.rooting.TriggerStrike(this, target);
    }

    public async Task<int> TakeDamage(Character dealer, int damage, DamageType damageType) {
        if (!stats.ability.humanShield.GetValue(stats)) {
            List<Character> nearbyFriends = gridManager.GetNearbyFriends(this);
            foreach (var nearbyFriend in nearbyFriends) {
                if (nearbyFriend.stats.ability.humanShield.GetValue(nearbyFriend.stats)) {
                    return await nearbyFriend.TakeDamage(dealer, damage, damageType);
                }
            }
        }

        damage = stats.ability.vulnerable.TriggerDamaged(this, damage);
        damage = stats.ability.sapPower.TriggerStrike(dealer, this, damage);
        damage = stats.ability.armor.TriggerDamaged(this, damage, damageType);
        damage = stats.ability.resistance.TriggerDamaged(this, damage, damageType);
        damage = stats.ability.thickSkin.TriggerDamaged(this, damage);

        damage = stats.ability.stoneskin.TriggerDamaged(this, damage);
        damage = stats.ability.incorporeal.TriggerDamaged(this, damage, damageType);

        damage = stats.ability.immune.TriggerDamaged(this, damage);

        List<Task> asyncFunctions = new();

        if (damage > 0) {
            stats.AddHealthCurrent(-damage);
            UpdateWarriorUI();

            if (stats.GetHealth() <= 0) {
                asyncFunctions.Add(Die(dealer));
            } else {
                stats.ability.vengeance.TriggerDamaged(this);
            }
        }

        dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.Red);

        asyncFunctions.Add(floatingText.CreateFloatingText(transform, damage.ToString(), ColorPalette.ColorEnum.Red, true));

        await Task.WhenAll(asyncFunctions);

        if (dealer) {
            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.White);
        }

        return damage;
    }

    public async Task Heal(Character dealer, int amount) {
        if (!this || !dealer) return;

        if (stats.GetHealth() < stats.GetHealthMax()) {
            if (stats.ability.bleeding.GetValue(stats)) {
                amount = 0;
            }

            if (amount > 0) {
                stats.AddHealthCurrent(amount);
                UpdateWarriorUI();

                dealer.stats.ability.inspire.Trigger(dealer);
            }



            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.Green);

            await floatingText.CreateFloatingText(transform, amount.ToString(), ColorPalette.ColorEnum.Green, true);

            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.White);
        }
    }

    public async Task Die(Character dealer) {
        if (stats.ability.cheatDeath.TriggerDamaged(this)) return;

        gameManager.RemoveCharacter(this);
        gridManager.RemoveCharacter(this);

        stats.ability.skeletal.TriggerDeath(this, summoner);
        stats.ability.forestStrength.TriggerDeath(this, gridManager);
        stats.ability.evilInspiration.TriggerDeath(this, gridManager);
        stats.ability.forestProtection.TriggerDeath(this, gridManager);
        stats.ability.massResistance.TriggerDeath(this, gridManager);
        stats.ability.massEnflame.TriggerDeath(this, gridManager);
        stats.ability.massSelfHarm.TriggerDeath(this, gridManager);
        stats.ability.massImmolate.TriggerDeath(this, gridManager);
        stats.ability.summoningSpirits.TriggerDeath(this, gridManager);

        List<Task> asyncFunctions = new() {
            stats.ability.explosion.TriggerDeath(this, gridManager),
            stats.ability.revive.TriggerDeath(this, characterSpawner),
            stats.ability.hydraSplit.TriggerDeath(this, characterSpawner),
            stats.ability.boneSpread.TriggerDeath(this, characterSpawner),
            stats.ability.phoenixAshes.TriggerDeath(this, characterSpawner),
        };

        if (stats.ability.afterlife.GetValue(stats)) {
            GameObject clone = Instantiate(gameObject, transform.position, Quaternion.identity, transform.parent);
            asyncFunctions.Add(stats.ability.afterlife.TriggerDeath(this, hand, summonerObject, clone));
        }

        if (dealer != this) {
            dealer.stats.ability.cannibalism.TriggerKill(dealer);
            asyncFunctions.Add(dealer.stats.ability.carnivore.TriggerKill(dealer, this));
            asyncFunctions.Add(dealer.stats.ability.raiseDead.TriggerKill(dealer, this, characterSpawner));

            List<Character> friends = gridManager.GetFriends(dealer.stats.alignment);
            foreach (Character friend in friends) {
                asyncFunctions.Add(friend.stats.ability.deathCall.Trigger(friend, this, characterSpawner));
            }

            List<Character> characters = gridManager.GetCharacters();
            foreach (Character character in characters) {
                asyncFunctions.Add(character.stats.ability.looting.Trigger(character, floatingText));
            }

            asyncFunctions.Add(dealer.stats.ability.possess.TriggerKill(dealer, this, characterSpawner));
            asyncFunctions.Add(dealer.stats.ability.greedyStrike.TriggerKill(dealer, floatingText));
            asyncFunctions.Add(dealer.stats.ability.lifeInDeath.TriggerKill(dealer, gridManager));
            asyncFunctions.Add(dealer.stats.ability.dragonRecruiter.TriggerKill(dealer, dealer.hand));
        }

        gameObject.SetActive(false);

        if (stats.alignment == CharacterSpawner.Alignment.Friend) {
            foreach (Item item in ItemManager.LoadItems()) {
                await item.UseOnWarriorDeath(summoner);
            }
        }

        if (stats.alignment == CharacterSpawner.Alignment.Enemy) {
            await ItemManager.enemyItem.UseOnWarriorDeath(summoner);
        }

        Destroy(gameObject);
        await Task.WhenAll(asyncFunctions);
    }

    public async Task EndTurn() {
        await stats.ability.hitAndRun.TriggerOverturn(this);
        stats.ability.poisonCloud.TriggerOverturn(this, gridManager);
        await stats.ability.cemeteryGates.TriggerOverturn(this, characterSpawner);
        await stats.ability.rebirth.TriggerOverturn(this, characterSpawner);
        await stats.ability.regeneration.TriggerOverturn(this);
        await stats.ability.sprout.TriggerOverturn(this, characterSpawner);
        await stats.ability.sapEnergy.TriggerOverturn(this, gridManager);
        await stats.ability.massHeal.TriggerOverturn(this, gridManager);
        await stats.ability.lushGrounds.TriggerOverturn(this, gridManager);
        await stats.ability.heal.TriggerOverturn(this, gridManager);
        await stats.ability.faeMagic.TriggerOverturn(this, summoner);
        await stats.ability.thunderstorm.TriggerOverturn(this, gridManager);
        await stats.ability.lightningBolt.TriggerOverturn(this, gridManager);
        await stats.ability.immolate.TriggerOverturn(this, gridManager, gameManager);
        await stats.ability.bloodPact.TriggerOverturn(this, gridManager, summoner);
        await stats.ability.scrollStudies.TriggerOverturn(this, hand);
        stats.ability.seduced.Trigger(this);
        stats.ability.stunned.Trigger(this);
        await stats.ability.poisoned.TriggerOverturn(this);
        await stats.ability.burning.TriggerOverturn(this);
        await stats.ability.artist.TriggerOverturn(this, gameManager);
        stats.ability.friendDiscount.TriggerOverturn(this, gridManager);
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
        if (hoverCard) {
            hoverCard.ShowCardFromBattlefield(stats, gridManager.GetCellPosition(gridIndex));
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (hoverCard) {
            hoverCard.HideCard();
        }
    }

    public async void OnClick() {
        await gridManager.SelectCell(gridIndex);
    }
}
