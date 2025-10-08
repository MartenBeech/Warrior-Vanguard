public class InternationalLibrary {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 0, 0 },
            health = new int[] { 8, 12 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Construct,
            rarity = CardRarity.Legendary,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.scrollStudies.Add();

        return stats;
    }
}