public class LocalBookkeeper {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            strength = new int[] { 2, 3 },
            health = new int[] { 3, 4 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Librarian,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.enlighten.Add(1);

        return stats;
    }
}