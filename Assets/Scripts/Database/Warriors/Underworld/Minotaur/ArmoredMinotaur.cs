public class ArmoredMinotaur {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            strength = new int[] { 3, 3 },
            health = new int[] { 4, 4 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Minotaur,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.armor.Add(1, 2);

        return stats;
    }
}