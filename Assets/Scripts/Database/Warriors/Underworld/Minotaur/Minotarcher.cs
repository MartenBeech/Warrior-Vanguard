public class Minotarcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 4, 4 },
            strength = new int[] { 3, 3 },
            health = new int[] { 4, 5 },
            speed = 2,
            range = 4,
            damageType = DamageType.Physical,
            race = Race.Minotaur,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;

        return stats;
    }
}