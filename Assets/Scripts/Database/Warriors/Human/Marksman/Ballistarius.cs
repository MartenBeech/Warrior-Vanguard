public class Ballistarius {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 5, 5 },
            strength = new int[] { 5, 7 },
            health = new int[] { 6, 7 },
            speed = 2,
            range = 5,
            damageType = DamageType.Physical,
            race = Race.Marksman,
            rarity = CardRarity.Rare,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.reload.Add();
        ability.knockBack.Add();
        ability.pierce.Add();

        return stats;
    }
}