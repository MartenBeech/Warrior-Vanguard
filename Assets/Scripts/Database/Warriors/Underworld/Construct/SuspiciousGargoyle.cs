public class SuspiciousGargoyle {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 3, 3 },
            strength = new int[] { 3, 4 },
            health = new int[] { 6, 7 },
            speed = 0,
            range = 1,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Construct,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();
        ability.backstab.Add();

        return stats;
    }
}