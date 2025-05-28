public class WanderingBirch {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            strength = new int[] { 1, 1 },
            health = new int[] { 1, 1 },
            speed = 1,
            range = 1,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Treant,
            rarity = CardRarity.Common,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.familiarGround.Add(2, 3);

        return stats;
    }
}