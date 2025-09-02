public class ZombieHydra {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 6, 7 },
            health = new int[] { 6, 7 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Zombie,
            rarity = CardRarity.Legendary,
            genre = Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.hydraSplit.Add();
        ability.revive.Add();

        return stats;
    }
}