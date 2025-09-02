public class Youngling {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 1, 1 },
            strength = new int[] { 1, 1 },
            health = new int[] { 3, 4 },
            speed = 1,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Dwarf,
            rarity = CardRarity.Common,
            genre = Warrior.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.resistance.Add(1);

        return stats;
    }
}