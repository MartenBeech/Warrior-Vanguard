public class ForbiddenLibrary {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 3, 3 },
            strength = new int[] { 0, 0 },
            health = new int[] { 5, 6 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Support,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.artist.Add(1);
        ability.burning.Add(3);

        return stats;
    }
}