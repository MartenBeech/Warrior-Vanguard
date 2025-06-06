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

    public void SetStats(SummonerStats stats) {
        this.stats.SetStats(stats);
        UpdateSummonerUI();
    }

    public async Task Damage(Character dealer, int damage, GridManager gridManager) {

        damage = dealer.stats.ability.stealth.TriggerStrike(dealer, damage);

        int damageAfterShield = damage;

        if (damageAfterShield > 0) {
            damageAfterShield -= stats.shield;
            stats.shield -= damage;

            if (stats.shield < 0) {
                stats.shield = 0;
                stats.health -= damageAfterShield;
                if (stats.title == "Angel") {
                    Angel.LoseHealth(damageAfterShield);
                }
            }

            UpdateSummonerUI();
        }

        Color currentColor = dealer.image.GetComponent<Image>().color;
        dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.red);

        List<Task> asyncFunctions = new();

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        asyncFunctions.Add(floatingText.CreateFloatingText(transform, damage.ToString()));

        dealer.image.GetComponent<Image>().color = currentColor;


        if (damageAfterShield > 0) {
            asyncFunctions.Add(dealer.stats.ability.lifeSteal.Trigger(dealer, damageAfterShield));
            asyncFunctions.Add(dealer.stats.ability.lifeTransfer.Trigger(dealer, damageAfterShield, gridManager));
        }

        await Task.WhenAll(asyncFunctions);

        dealer.stats.ability.bloodlust.Trigger(dealer);
        await dealer.stats.ability.hitAndRun.Trigger(dealer);

        if (stats.health <= 0) {
            //TODO: Summoners should have a bool for isFriendly in case we will make different summoners to choose from.
            if (stats.title == "Angel") {
                LevelManager.LoseLevel();
            } else {
                LevelManager.CompleteLevel();
            }
        }
    }

    public void Heal(int amount) {
        if (stats.title == "Angel") {
            Angel.GainHealth(amount);
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
        await floatingText.CreateFloatingText(transform, $"+{amount} shield", ColorPalette.ColorEnum.gray);
    }
}