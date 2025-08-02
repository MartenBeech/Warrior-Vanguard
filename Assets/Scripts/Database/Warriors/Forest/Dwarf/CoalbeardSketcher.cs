public class CoalbeardSketcher {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 3 },
            strength = new int[] { 1, 1 },
            health = new int[] { 2, 2 },
            speed = 1,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Dwarf,
            rarity = CardRarity.Common,
            genre = Character.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.resistance.Add(1);
        ability.drawing.Add(2);

        return stats;
    }
}