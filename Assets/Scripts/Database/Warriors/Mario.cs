public class Mario : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mario",
            strength = new int[] { 1, 4 },
            health = new int[] { 10, 4 },
            cost = new int[] { 0, 0 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.heal.Add(2);

        return stats;
    }
}