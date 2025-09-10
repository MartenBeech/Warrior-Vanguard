public class FavoriteChild {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 3, 4 },
            health = new int[] { 2, 2 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Imp,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.familyDiscount.Add(1);

        return stats;
    }
}