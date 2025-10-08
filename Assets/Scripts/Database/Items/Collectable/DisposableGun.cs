using System.Collections.Generic;
using System.Threading.Tasks;

public class DisposableGun : Item {
    public override Item GetItem() {
        title = GetType().Name;
        description = "When a friend is summoned, it strikes a random enemy";
        rarity = ItemRarity.Boss;
        genre = Genre.None;
        return this;
    }

    public override async Task UseAfterFriendSummon(ItemTriggerParams parameters) {
        GridManager gridManager = FindFirstObjectByType<GridManager>();
        List<Warrior> enemies = gridManager.GetEnemies(parameters.stats.alignment);

        Warrior randomEnemy = Rng.Entry(enemies);
        if (randomEnemy == null) return;

        await randomEnemy.TakeDamage(parameters.warrior, parameters.stats.GetStrength(), parameters.stats.damageType);
    }
}