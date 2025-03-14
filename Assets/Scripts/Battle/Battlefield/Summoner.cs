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
        tooltipManager.transform.position = new Vector2(transform.position.x, transform.position.y + 200);
        tooltipManager.AddTooltip(stats.title, stats.description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipManager.RemoveTooltips();
    }

    public void UpdateSummonerUI() {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{stats.title}");
        healthText.GetComponent<TMP_Text>().text = $"{stats.health}/{stats.healthMax}";
    }

    public void SetStats(SummonerStats stats) {
        this.stats.SetStats(stats);
        UpdateSummonerUI();
    }

    public async Task Damage(Character dealer, int damage) {
        if (dealer.stats.ability.stealth.TriggerAttack(dealer)) {
            damage *= 2;
        }

        if (damage > 0) {
            stats.health -= damage;
            UpdateSummonerUI();
            dealer.stats.ability.bloodlust.Trigger(dealer);
        }

        FloatingText floatingText = FindFirstObjectByType<FloatingText>();
        await floatingText.CreateFloatingText(transform, damage.ToString());

        if (damage > 0) {
            await dealer.stats.ability.lifeSteal.Trigger(dealer, damage);
        }

        if (stats.health <= 0) {
            LevelManager.CompleteLevel();
        }
    }

    public void Heal(int amount) {
        stats.health += amount;
        if (stats.health > stats.healthMax) {
            stats.health = stats.healthMax;
        }
        UpdateSummonerUI();
    }
}