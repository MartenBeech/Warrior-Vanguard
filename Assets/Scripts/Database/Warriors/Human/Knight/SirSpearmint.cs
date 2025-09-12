public class SirSpearmint {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 5,
            cost = new int[] { 8, 8 },
            strength = new int[] { 2, 4 },
            health = new int[] { 12, 14 },
            speed = 4,
            range = 2,
            damageType = DamageType.Physical,
            race = Race.Knight,
            rarity = CardRarity.Legendary,
            genre = Genre.Human,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.joust.Add();
        ability.pierce.Add();
        ability.hitAndRun.Add();

        return stats;
    }
}