public class RadiantProtector {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 8, 8 },
            strength = new int[] { 3, 3 },
            health = new int[] { 17, 20 },
            speed = 4,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Unicorn,
            genre = Character.Genre.Forest
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.resistance.Add(1, 2);
        ability.massResistance.Add(1, 2);

        return stats;
    }
}