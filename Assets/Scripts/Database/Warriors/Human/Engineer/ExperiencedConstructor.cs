public class ExperiencedConstructor {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 3, 5 },
            health = new int[] { 9, 9 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Engineer,
            rarity = CardRarity.Rare,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.repair.Add(3, 5);
        ability.demolish.Add();
        ability.builder.Add();

        return stats;
    }
}