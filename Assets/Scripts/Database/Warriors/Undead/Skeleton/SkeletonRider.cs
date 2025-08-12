public class SkeletonRider {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 1 },
            health = new int[] { 3, 3 },
            speed = 4,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Skeleton,
            rarity = CardRarity.Rare,
            genre = Character.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.skeletal.Add(1, 2);
        ability.bloodlust.Add(1, 2);

        return stats;
    }
}