public class Stormseer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 7, 7 },
            strength = new int[] { 2, 2 },
            health = new int[] { 8, 8 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
            race = Character.Race.Sorcerer,
            rarity = CardRarity.Legendary,
            genre = Character.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.lightningBolt.Add(3, 4);
        ability.thunderstorm.Add(1, 2);

        return stats;
    }
}