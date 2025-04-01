public class BoneConjurer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 5,
            strength = new int[] { 3, 4 },
            health = new int[] { 7, 8 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.raiseDead.Add();
        ability.boneSculptor.Add(3, 4);

        return stats;
    }
}