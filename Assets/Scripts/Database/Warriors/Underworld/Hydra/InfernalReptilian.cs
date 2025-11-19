public class InfernalReptilian {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 6, 6 },
            strength = new int[] { 4, 5 },
            health = new int[] { 10, 10 },
            speed = 2,
            range = 1,
            damageType = DamageType.Magical,
            race = Race.Hydra,
            rarity = CardRarity.Rare,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.whirlwind.Add();
        ability.enflame.Add();

        return stats;
    }
}