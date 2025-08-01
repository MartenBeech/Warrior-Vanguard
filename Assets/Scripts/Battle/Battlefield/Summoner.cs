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

    public async Task TakeDamage(Character dealer, int damage, GridManager gridManager, Character.DamageType damageType) {
        if (dealer) {
            damage = dealer.stats.ability.stealth.Trigger(dealer, damage);
            damage = stats.ability.armor.Trigger(dealer, damage, damageType);
            damage = stats.ability.resistance.Trigger(dealer, damage, damageType);
        }

        int damageAfterShield = damage;

        if (damageAfterShield > 0) {
            damageAfterShield -= stats.shield;
            stats.shield -= damage;

            if (stats.shield < 0) {
                stats.shield = 0;
                stats.health -= damageAfterShield;
                if (stats.isFriendly) {
                    FriendlySummoner.LoseHealth(damageAfterShield);
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
                dealer.stats.ability.selfHarm.Trigger(dealer)
            };
            await Task.WhenAll(asyncFunctions);

            if (!dealer) return;

            dealer.image.GetComponent<Image>().color = currentColor;

            if (damageAfterShield > 0) {
                await dealer.stats.ability.lifeSteal.Trigger(dealer, damageAfterShield);
                await dealer.stats.ability.lifeTransfer.Trigger(dealer, damageAfterShield, gridManager);
            }

            dealer.stats.ability.bloodlust.Trigger(dealer);
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

    public async Task EndTurn(CharacterSpawner characterSpawner) {
        if (stats.ability.summonWisp.GetValue(stats)) {
            await stats.ability.summonWisp.Trigger(this, characterSpawner);
        }
    }
}