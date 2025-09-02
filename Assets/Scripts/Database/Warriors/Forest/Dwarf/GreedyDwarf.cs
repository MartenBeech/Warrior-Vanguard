public class GreedyDwarf {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 5 },
            health = new int[] { 4, 6 },
            speed = 1,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Dwarf,
            rarity = CardRarity.Legendary,
            genre = Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.resistance.Add(2);
        ability.greedyStrike.Add(25, 50);

        return stats;
    }
}