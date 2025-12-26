public class Centarcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            strength = new int[] { 3, 4 },
            health = new int[] { 1, 1 },
            speed = 4,
            range = 4,
            damageType = DamageType.Physical,
            race = Race.Centaur,
            rarity = CardRarity.Common,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        return stats;
    }
}