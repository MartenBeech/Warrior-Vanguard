public class InternationalLibrary {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 0, 0 },
            health = new int[] { 8, 11 },
            speed = 0,
            range = 0,
            damageType = DamageType.Physical,
            race = Race.Construct,
            rarity = CardRarity.Rare,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.scrollStudies.Add();

        return stats;
    }
}