public class PileOfBones {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 0, 0 },
            health = new int[] { 4, 5 },
            speed = 0,
            range = 0,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Construct,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.boneSpread.Add();

        return stats;
    }
}