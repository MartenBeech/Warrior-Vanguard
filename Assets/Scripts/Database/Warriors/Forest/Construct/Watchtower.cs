public class Watchtower {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 5 },
            health = new int[] { 4, 5 },
            speed = 0,
            range = 4,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Construct,
            rarity = CardRarity.Common,
            genre = Character.Genre.Forest,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.construct.Add();

        return stats;
    }
}