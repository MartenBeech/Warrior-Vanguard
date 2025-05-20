public class MultibowNovice {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 1,
            strength = new int[] { 1, 2 },
            health = new int[] { 1, 1 },
            speed = 2,
            range = 5,
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