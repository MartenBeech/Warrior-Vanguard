public class TheOriginal {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 8, 8 },
            strength = new int[] { 3, 4 },
            health = new int[] { 10, 12 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Vampire,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.lifeSteal.Add();
        ability.retaliate.Add();
        ability.bleed.Add();

        return stats;
    }
}