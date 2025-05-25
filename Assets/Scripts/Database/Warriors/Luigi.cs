public class Luigi : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Luigi",
            strength = new int[] { 12, 13 },
            health = new int[] { 10, 12 },
            cost = new int[] { 1, 1 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;

        return stats;
    }
}