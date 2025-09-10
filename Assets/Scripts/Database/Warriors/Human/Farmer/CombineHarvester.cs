public class CombineHarvester {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 2,
            cost = new int[] { 7, 7 },
            strength = new int[] { 3, 3 },
            health = new int[] { 12, 15 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Farmer,
            rarity = CardRarity.Legendary,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.farming.Add(3, 4);

        return stats;
    }
}