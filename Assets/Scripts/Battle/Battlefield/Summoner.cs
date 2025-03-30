using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading.Tasks;

public class Summoner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public SummonerStats stats = new();
    public GameObject healthText;
    public GameObject image;
    public TooltipManager tooltipManager;
    public void OnPointerEnter(PointerEventData eventData) {
        if (tooltipManager) {
            tooltipManager.transform.position = new Vector2(transform.position.x, transform.position.y + 200);
            tooltipManager.AddTooltip(stats.title, stats.description);
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (tooltipManager) {
            tooltipManager.RemoveTooltips();
        }
    }

    public void UpdateSummonerUI() {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{stats.title}");
        healthText.GetComponent<TMP_Text>().text = $"{stats.health}/{stats.healthMax}";
    }

    public void SetStats(SummonerStats stats) {
        this.stats.SetStats(stats);
        UpdateSummonerUI();
    }

    public async Task Damage(Character dealer, int damage, GridManager gridManager) {

        damage = dealer.stats.ability.stealth.TriggerAttack(dealer, damage);

        if (damage > 0) {
            stats.health -= damage;
            UpdateSummonerUI();
            dealer.stats.ability.bloodlust.Trigger(dealer);

            if (stats.title == "Angel") {
                Angel.LoseHealth(damage);
            }
        }

        Color currentColor = dealer.image.GetComponent<Image>().color;
        dealer.image.GetComponent<Image>().color = ColorPalette.GetColor(ColorPalette.ColorEnum.red);

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(transform, damage.ToString());

        dealer.image.GetComponent<Image>().color = currentColor;


        if (damage > 0) {
            await dealer.stats.ability.lifeSteal.Trigger(dealer, damage);
            await dealer.stats.ability.lifeTransfer.Trigger(dealer, damage, gridManager);
        }

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
}