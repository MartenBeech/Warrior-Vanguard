public class Luigi : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Luigi",
            strength = new int[] { 1, 13 },
            health = new int[] { 5, 12 },
            cost = new int[] { 10, 1 },
            speed = 2,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.Human,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.afterlife.Add();

        return stats;
    }
}