public class Mario : WarriorStats {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = "Mario",
            cost = new int[] { 0, 0 },
            strength = new int[] { 1, 4 },
            health = new int[] { 10, 4 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.None,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.darkTouch.Add(10);
        ability.retaliate.Add();

        return stats;
    }
}