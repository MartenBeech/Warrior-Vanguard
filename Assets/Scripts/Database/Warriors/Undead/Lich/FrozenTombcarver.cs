public class FrozenTombcarver {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            strength = new int[] { 1, 2 },
            health = new int[] { 11, 11 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Lich,
            rarity = CardRarity.Rare,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.splash.Add();
        ability.raiseDead.Add();
        ability.frozenTouch.Add();

        return stats;
    }
}