public class DruidOfTheStorm {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 5, 6 },
            health = new int[] { 3, 5 },
            speed = 2,
            range = 4,
            damageType = DamageType.Magical,
            race = Race.Druid,
            rarity = CardRarity.Rare,
            genre = Genre.Elves,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.swap.Add();
        ability.knockBack.Add();
        ability.frozenTouch.Add();

        return stats;
    }
}