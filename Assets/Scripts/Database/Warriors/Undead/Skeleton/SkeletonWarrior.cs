public class SkeletonWarrior {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 1, 1 },
            health = new int[] { 3, 3 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Skeleton,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.skeletal.Add(1, 2);
        ability.armor.Add(1, 2);

        return stats;
    }
}