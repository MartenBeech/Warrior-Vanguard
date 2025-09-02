public class TheFourHorsemen {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 4, 5 },
            health = new int[] { 4, 5 },
            speed = 4,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Nightrider,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.cheatDeath.Add();
        ability.soulSiphon.Add();
        ability.spawn.Add(3);

        return stats;
    }
}