public class SkyGlider {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 2, 2 },
            strength = new int[] { 2, 3 },
            health = new int[] { 5, 6 },
            speed = 4,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Griffin,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.retaliate.Add();

        return stats;
    }
}