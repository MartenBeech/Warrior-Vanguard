public class LongbowGrandmaster {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 7,
            strength = new int[] { 3, 4 },
            health = new int[] { 4, 4 },
            speed = 2,
            range = 9,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Elf,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.multishot.Add();

        return stats;
    }
}