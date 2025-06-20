public class WoodElf {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 3, 4 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 5,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Elf,
            genre = Character.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;

        return stats;
    }
}