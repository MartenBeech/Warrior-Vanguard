public class SkeletonArcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 3, 3 },
            health = new int[] { 1, 1 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Skeleton,
            genre = Character.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.skeletal.Add(1, 2);
        ability.poison.Add(1, 2);

        return stats;
    }
}