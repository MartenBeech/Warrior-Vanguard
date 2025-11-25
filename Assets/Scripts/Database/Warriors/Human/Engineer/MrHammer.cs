public class MrHammer {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 9, 9 },
            strength = new int[] { 4, 6 },
            health = new int[] { 18, 20 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Engineer,
            rarity = CardRarity.Legendary,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.massRepair.Add(4, 6);
        ability.demolish.Add();
        ability.builder.Add();
        ability.massBuilder.Add();

        return stats;
    }
}