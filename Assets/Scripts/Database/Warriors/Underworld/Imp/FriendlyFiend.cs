public class FriendlyFiend {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 2 },
            health = new int[] { 1, 1 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Imp,
            rarity = CardRarity.Common,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.friendDiscount.Add(1);

        return stats;
    }
}