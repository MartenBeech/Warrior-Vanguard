using System.Threading.Tasks;
using UnityEngine;

public class ForbiddenLifeElixir : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "Your warriors with Revive or Afterlife have both";
        return this;
    }

    public override void UseOnFriendSpawn(WarriorStats stats) {
        if (stats.ability.GetAbilityText(stats).Contains("Revive") && !stats.ability.GetAbilityText(stats).Contains("Afterlife")) {
            stats.ability.afterlife.Add();
        } else if (stats.ability.GetAbilityText(stats).Contains("Afterlife") && !stats.ability.GetAbilityText(stats).Contains("Revive")) {
            stats.ability.revive.Add();
        }
    }
}