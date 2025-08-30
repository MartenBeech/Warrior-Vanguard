using System.Threading.Tasks;
using UnityEngine;

public class ForbiddenLifeElixir : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your warriors with Revive or Afterlife have both";
        rarity = ItemManager.Rarity.None;
        return this;
    }

    public override void UseOnFriendSpawn(ItemTriggerParams parameters) {
        if (parameters.stats.ability.GetAbilityText(parameters.stats).Contains("Revive") && !parameters.stats.ability.GetAbilityText(parameters.stats).Contains("Afterlife")) {
            parameters.stats.ability.afterlife.Add();
        } else if (parameters.stats.ability.GetAbilityText(parameters.stats).Contains("Afterlife") && !parameters.stats.ability.GetAbilityText(parameters.stats).Contains("Revive")) {
            parameters.stats.ability.revive.Add();
        }
    }
}