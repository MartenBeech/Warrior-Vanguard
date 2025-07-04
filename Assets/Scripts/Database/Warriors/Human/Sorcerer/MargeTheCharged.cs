public class MargeTheCharged {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 2, 2 },
            health = new int[] { 3, 3 },
            speed = 2,
            range = 4,
            damageType = Character.DamageType.Magical,
            race = Character.Race.Sorcerer,
            rarity = CardRarity.Rare,
            genre = Character.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.lightningBolt.Add(2, 3);
        ability.staticEntrance.Add(2, 3);

        return stats;
    }
}