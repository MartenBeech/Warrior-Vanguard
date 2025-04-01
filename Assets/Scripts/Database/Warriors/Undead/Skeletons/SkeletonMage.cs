public class SkeletonMage {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 2,
            strength = new int[] { 2, 3 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.skeletal.Add();
        ability.weaken.Add(1);

        return stats;
    }
}