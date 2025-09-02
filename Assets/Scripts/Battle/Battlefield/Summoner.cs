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
        } else {
            shieldImage.SetActive(false);
        }
    }

    void UpdateItem() {
        if (itemImage != null) {
            if (!stats.isFriendly && ItemManager.enemyItem != null) {
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

    public async Task TakeDamage(Warrior dealer, int damage, GridManager gridManager, Warrior.DamageType damageType) {
        if (dealer) {
            damage = dealer.stats.ability.stealth.TriggerStrike(dealer, damage);
            damage = stats.ability.armor.TriggerDamaged(dealer, damage, damageType);
            damage = stats.ability.resistance.TriggerDamaged(dealer, damage, damageType);
        }

        if (!stats.isFriendly && damage == 1) {
            Underdog underdog = new GameObject().AddComponent<Underdog>();
            if (ItemManager.items.Find(item => item.title == underdog.GetItem().title)) {
                damage = 2;
            }
        }

        int damageAfterResistances = damage;


        if (damageAfterResistances > 0) {
            damageAfterResistances -= stats.shield;
            stats.shield -= damage;

            if (stats.shield < 0) {
                stats.shield = 0;
                stats.health -= damageAfterResistances;
                if (stats.isFriendly) {
                    FriendlySummoner.LoseHealth(damageAfterResistances);
                }
            }

            UpdateSummonerUI();
        }

        if (dealer) {
            Color currentColor = dealer.image.GetComponent<Image>().color;
            dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.Red);

            FloatingText floatingText = FindFirstObjectByType<FloatingText>();
            List<Task> asyncFunctions = new() {
                floatingText.CreateFloatingText(transform, damage.ToString()),
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
            await floatingText.CreateFloatingText(transform, damage.ToString());
        }

        if (stats.health <= 0) {
            if (stats.isFriendly) {
                LevelManager.LoseLevel();
            } else {
                LevelManager.CompleteLevel();
            }
        }
    }

    public void Heal(int amount) {
        if (stats.isFriendly) {
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
        await floatingText.CreateFloatingText(transform, $"+{amount} shield", ColorPalette.ColorEnum.Gray);
    }

    public async Task EndTurn(WarriorSummoner warriorSummoner) {
        if (stats.ability.summonWisp.GetValue(stats)) {
            await stats.ability.summonWisp.TriggerOverturn(this, warriorSummoner);
        }
    }
}