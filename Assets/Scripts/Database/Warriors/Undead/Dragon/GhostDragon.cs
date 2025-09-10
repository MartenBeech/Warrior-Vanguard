public class GhostDragon {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 8, 8 },
            strength = new int[] { 6, 6 },
            health = new int[] { 8, 10 },
            speed = 2,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.Dragon,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.pierce.Add();
        ability.afterlife.Add();
        ability.eternalNightmare.Add();

        return stats;
    }
}