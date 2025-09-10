public class ThirdEyeHarpy {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 2, 2 },
            strength = new int[] { 4, 5 },
            health = new int[] { 2, 3 },
            speed = 2,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Harpy,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.backstab.Add();

        return stats;
    }
}