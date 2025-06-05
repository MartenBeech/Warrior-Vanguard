public class LuckyLooter {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 4, 5 },
            health = new int[] { 3, 4 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Pirate,
            rarity = CardRarity.Legendary,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.looting.Add(10, 15);

        return stats;
    }
}