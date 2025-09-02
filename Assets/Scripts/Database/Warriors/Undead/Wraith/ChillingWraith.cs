public class ChillingWraith {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 3, 5 },
            health = new int[] { 3, 3 },
            speed = 2,
            range = 4,
            damageType = DamageType.Physical,
            race = Race.Wraith,
            rarity = CardRarity.Common,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Legendary;

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();
        ability.frozenTouch.Add();

        return stats;
    }
}