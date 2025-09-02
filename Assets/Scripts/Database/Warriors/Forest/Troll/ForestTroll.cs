public class ForestTroll {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 6, 6 },
            strength = new int[] { 5, 5 },
            health = new int[] { 6, 7 },
            speed = 2,
            range = 2,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Troll,
            rarity = CardRarity.Rare,
            genre = Warrior.Genre.Forest
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.stoneskin.Add();
        ability.regeneration.Add(1, 2);

        return stats;
    }
}