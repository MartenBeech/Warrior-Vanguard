public class LichQueen {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 9,
            strength = new int[] { 3, 4 },
            health = new int[] { 14, 16 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.splash.Add();
        ability.deathCall.Add();

        return stats;
    }
}