public class HoodyRobin {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            cost = new int[] { 5, 5 },
            strength = new int[] { 3, 4 },
            health = new int[] { 3, 3 },
            speed = 2,
            range = 5,
            damageType = Warrior.DamageType.Physical,
            race = Warrior.Race.Human,
            rarity = CardRarity.Rare,
            genre = Warrior.Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.multishot.Add();

        return stats;
    }
}