public class VoidBeing {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 7,
            strength = new int[] { 5, 6 },
            health = new int[] { 4, 5 },
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
        ability.incorporeal.Add();
        ability.darkTouch.Add(5, 6);

        return stats;
    }
}