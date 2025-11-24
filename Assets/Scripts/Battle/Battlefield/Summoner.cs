using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;

public class Summoner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public SummonerStats stats = new();
    public GameObject healthText;
    public GameObject shieldText;
    public GameObject shieldImage;
    public GameObject itemImage;
    public TooltipManager tooltipManager;
    public void OnPointerEnter(PointerEventData eventData) {
        tooltipManager.transform.position = new Vector2(transform.position.x, transform.position.y + 200);
        tooltipManager.AddTooltip(stats.displayTitle, stats.description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.RemoveTooltips();
    }

    public void UpdateSummonerUI() {
        GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{stats.title}");
        healthText.GetComponent<TMP_Text>().text = $"{stats.health}/{stats.healthMax}";
        if (stats.shield > 0) {
            shieldImage.SetActive(true);
            shieldText.GetComponent<TMP_Text>().text = stats.shield.ToString();
            if (stats.shield >= 20 && stats.alignment == Alignment.Friend) {
                PlayerPrefs.SetInt(PlayerPrefsKeys.safetyFirst, 1);
            }
        } else {
            shieldImage.SetActive(false);
        }
    }

    void UpdateItem() {
        if (itemImage != null) {
            if (stats.alignment == Alignment.Enemy && ItemManager.enemyItem != null) {
                itemImage.SetActive(true);
                itemImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Items/{ItemManager.enemyItem.title}");
            } else {
                itemImage.SetActive(false);
            }
        }
    }

    public void SetStats(SummonerStats stats) {
        this.stats.SetStats(stats);
        UpdateSummonerUI();
        UpdateItem();
    }

    public async Task TakeDamage(Warrior dealer, int damage, GridManager gridManager, DamageType damageType, GameManager gameManager = null) {
        if (dealer) {
            damage = dealer.stats.ability.stealth.TriggerStrike(dealer, damage);
            damage = stats.ability.armor.TriggerDamaged(dealer, damage, damageType);
            damage = stats.ability.resistance.TriggerDamaged(dealer, damage, damageType);
        }

        if (stats.alignment == Alignment.Enemy && damage == 1) {
            Underdog underdog = new GameObject().AddComponent<Underdog>();
            Item item = ItemManager.items.Find(item => item.title == underdog.GetItem().title);
            if (item) {
                damage++;
            }
        }

        if (stats.alignment == Alignment.Enemy) {
            PeacefulPigeon peacefulPigeon = new GameObject().AddComponent<PeacefulPigeon>();
            Item item = ItemManager.items.Find(item => item.title == peacefulPigeon.GetItem().title);
            if (item) {
                item.triggeredThisTurn = true;
            }
        }

        if (stats.alignment == Alignment.Friend && damage >= 2) {
            RuneStone runeStone = new GameObject().AddComponent<RuneStone>();
            Item item = ItemManager.items.Find(item => item.title == runeStone.GetItem().title);
            if (item && !item.triggeredThisTurn) {
                damage = 1;
                item.triggeredThisTurn = true;
            }

        }

        int damageAfterResistances = damage;


        if (damageAfterResistances > 0) {
            damageAfterResistances -= stats.shield;
            stats.shield -= damage;

            if (stats.shield < 0) {
                stats.shield = 0;
                stats.health -= damageAfterResistances;
                if (stats.alignment == Alignment.Friend) {
                    FriendlySummoner.LoseHealth(damageAfterResistances);

                    //Achievement
                    PlayerPrefs.SetInt(PlayerPrefsKeys.flawless_helper, 0);
                    PlayerPrefs.Save();
                }
            }

            UpdateSummonerUI();
        }

        if (dealer) {
            Color currentColor = dealer.image.GetComponent<Image>().color;
            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorEnum.Red);

            FloatingText floatingText = FindFirstObjectByType<FloatingText>();
            List<Task> asyncFunctions = new() {
                floatingText.CreateFloatingText(transform, damage.ToString(), ColorEnum.Red, false),
                dealer.stats.ability.selfHarm.TriggerAttack(dealer)
            };
            await Task.WhenAll(asyncFunctions);

            if (!dealer) return;

            dealer.image.GetComponent<Image>().color = currentColor;

            if (damageAfterResistances > 0) {
                await dealer.stats.ability.lifeSteal.TriggerStrike(dealer, damageAfterResistances);
                await dealer.stats.ability.lifeTransfer.TriggerStrike(dealer, damageAfterResistances, gridManager);
            }
        } else {
            FloatingText floatingText = FindFirstObjectByType<FloatingText>();
            await floatingText.CreateFloatingText(transform, damage.ToString(), ColorEnum.Red, false);
        }

        if (stats.health <= 0) {
            if (stats.alignment == Alignment.Friend) {
                LevelManager.LoseLevel();
            } else {
                //Achievement
                if (gameManager && gameManager.friendlyDeaths == 0) {
                    PlayerPrefs.SetInt(PlayerPrefsKeys.saveEveryResource, 1);
                }

                //Achievement
                if (PlayerPrefs.GetInt(PlayerPrefsKeys.livingOnTheEdge_helper, 0) == 1) {
                    PlayerPrefs.SetInt(PlayerPrefsKeys.livingOnTheEdge, 1);
                }

                //Achievement
                if (PlayerPrefs.GetInt(PlayerPrefsKeys.flawless_helper, 0) == 1) {
                    PlayerPrefs.SetInt(PlayerPrefsKeys.flawless, 1);
                }

                //Achievement
                if (PlayerPrefs.GetInt(PlayerPrefsKeys.heroPowerDeactivated_helper, 0) == 1) {
                    PlayerPrefs.SetInt(PlayerPrefsKeys.heroPowerDeactivated, 1);
                }

                PlayerPrefs.SetInt(PlayerPrefsKeys.livingOnTheEdge_helper, 0);
                PlayerPrefs.SetInt(PlayerPrefsKeys.flawless_helper, 0);
                PlayerPrefs.SetInt(PlayerPrefsKeys.heroPowerDeactivated_helper, 0);
                PlayerPrefs.Save();

                LevelManager.CompleteLevel();
            }
        }

        if (stats.alignment == Alignment.Friend && damageType == DamageType.Physical) {
            VoodooDoll voodooDoll = new GameObject().AddComponent<VoodooDoll>();
            Item item = ItemManager.items.Find(item => item.title == voodooDoll.GetItem().title);
            if (item) {
                await gameManager.enemySummoner.TakeDamage(null, 1, gridManager, DamageType.Magical);
            }
        }
    }

    public void Heal(int amount) {
        if (stats.alignment == Alignment.Friend) {
            FriendlySummoner.GainHealth(amount);
        }

        stats.health += amount;
        if (stats.health > stats.healthMax) {
            stats.health = stats.healthMax;
        }
        UpdateSummonerUI();
    }

    public async Task AddShield(int amount) {
        stats.shield += amount;
        UpdateSummonerUI();

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(transform, $"+{amount} shield", ColorEnum.Gray, false);
    }

    public async Task EndTurn(WarriorSummoner warriorSummoner) {
        if (stats.ability.summonWisp.GetValue(stats)) {
            await stats.ability.summonWisp.TriggerOverturn(this, warriorSummoner);
        }
    }
}