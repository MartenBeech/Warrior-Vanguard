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
        tooltipManager.AddTooltip(stats.title, stats.description);
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

        damage = dealer.stats.ability.stealth.TriggerAttack(dealer, damage);

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
            dealer.stats.ability.bloodlust.Trigger(dealer);
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

        if (stats.health <= 0) {
            switch (stats.title) {
                case "Angel":
                    LevelManager.LoseLevel();
                    return;
                case "Devil":
                    LevelManager.CompleteLevel();
                    return;
                default:
                    LevelManager.CompleteLevel();
                    return;
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

    public void AddShield(int amount) {
        stats.shield += amount;
        UpdateSummonerUI();
    }
}