public class ImpressiveArtist {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            strength = new int[] { 1, 1 },
            health = new int[] { 1, 3 },
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
        ability.artist.Add(1);

        return stats;
    }
}