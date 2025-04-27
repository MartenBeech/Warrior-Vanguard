public class VileMutation {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = 7,
            strength = new int[] { 0, 0 },
            health = new int[] { 9, 11 },
            speed = 0,
            range = 0,
            damageType = Character.DamageType.Physical,
            classType = Character.ClassType.Undead,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.poisonCloud.Add(3, 4);
        ability.poisoningAura.Add(3, 4);

        return stats;
    }
}