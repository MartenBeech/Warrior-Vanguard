public class AngelOfDeath {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 9, 9 },
            strength = new int[] { 4, 6 },
            health = new int[] { 19, 21 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Reaper,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.reckoning.Add(4, 6);

        return stats;
    }
}