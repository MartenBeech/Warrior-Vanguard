public class Knight {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 4, 4 },
            strength = new int[] { 4, 4 },
            health = new int[] { 4, 4 },
            speed = 2,
            range = 2,
            damageType = Character.DamageType.Physical,
            race = Character.Race.Human,
            rarity = CardRarity.Rare,
            genre = Character.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.armor.Add(2, 3);

        return stats;
    }
}