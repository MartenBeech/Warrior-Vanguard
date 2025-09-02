public class ElvenArcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 2, 3 },
            health = new int[] { 2, 2 },
            speed = 2,
            range = 5,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Elf,
            genre = Warrior.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.doubleStrike.Add();

        return stats;
    }
}