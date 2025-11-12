public class MargeTheCharged {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            strength = new int[] { 0, 0 },
            health = new int[] { 2, 2 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Sorcerer,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.lightningBolt.Add(3, 5);

        return stats;
    }
}