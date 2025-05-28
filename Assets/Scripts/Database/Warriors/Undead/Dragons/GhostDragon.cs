public class GhostDragon {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 8, 8 },
            strength = new int[] { 6, 6 },
            health = new int[] { 8, 8 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Magical,
            race = Character.Race.Dragon,
            rarity = CardRarity.Legendary,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.pierce.Add();
        ability.afterlife.Add();
        ability.eternalNightmare.Add();

        return stats;
    }
}