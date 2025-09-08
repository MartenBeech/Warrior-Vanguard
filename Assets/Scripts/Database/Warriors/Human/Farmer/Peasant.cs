public class Peasant {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 0, 0 },
            strength = new int[] { 1, 1 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Farmer,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;

        return stats;
    }
}