public class WatchfulGuard {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 5 },
            health = new int[] { 4, 4 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Human,
            rarity = CardRarity.Common,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.armor.Add(1, 2);
        ability.guard.Add();

        return stats;
    }
}