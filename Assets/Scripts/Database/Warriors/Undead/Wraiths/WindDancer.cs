public class WindDancer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 1,
            strength = new int[] { 2, 3 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }
        stats.rarity = CardRarity.Legendary;

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();

        return stats;
    }
}