public class ZombieHydra {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 9, 9 },
            strength = new int[] { 6, 9 },
            health = new int[] { 6, 9 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Zombie,
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