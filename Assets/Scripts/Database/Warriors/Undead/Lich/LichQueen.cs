public class LichQueen {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 3, 4 },
            health = new int[] { 10, 12 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Lich,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.splash.Add();
        ability.deathCall.Add();

        return stats;
    }
}