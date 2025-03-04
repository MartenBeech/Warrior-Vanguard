using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Summoner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public SummonerStats stats = new();
    public GameObject healthText;
    public GameObject image;
    public TooltipHandler tooltipHandler;
    public void OnPointerEnter(PointerEventData eventData) {
        tooltipHandler.ShowTooltip(stats.title, stats.description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltipHandler.HideTooltip();
    }

    public void UpdateSummonerUI() {
        image.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Images/Summoners/{stats.title}");
        healthText.GetComponent<TMP_Text>().text = $"{stats.health}/{stats.healthMax}";
    }

    public void SetStats(SummonerStats stats) {
        this.stats.SetStats(stats);
        UpdateSummonerUI();
    }

    public void Damage(int amount) {
        stats.health -= amount;
        UpdateSummonerUI();
    }

    public void Heal(int amount) {
        stats.health += amount;
        if (stats.health > stats.healthMax) {
            stats.health = stats.healthMax;
        }
        UpdateSummonerUI();
    }
}