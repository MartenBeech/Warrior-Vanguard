public class LightPiercer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 5 },
            health = new int[] { 6, 7 },
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
        ability.resistance.Add(2);
        ability.pierce.Add();

        return stats;
    }
}