public class WindDancer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 1, 1 },
            strength = new int[] { 2, 2 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 2,
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

        return stats;
    }
}