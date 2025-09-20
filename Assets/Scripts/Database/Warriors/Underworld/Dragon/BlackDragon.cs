public class BlackDragon {
    public WarriorStats GetStats() {
        WarriorStats stats = new() {
            title = GetType().Name,
            levelUnlocked = 1,
            cost = new int[] { 8, 8 },
            strength = new int[] { 6, 6 },
            health = new int[] { 8, 10 },
            speed = 2,
            range = 2,
            damageType = DamageType.Magical,
            race = Race.Dragon,
            rarity = CardRarity.Legendary,
            genre = Genre.Underworld,
        };
        for (int i = 0; i < 2; i++) {
            stats.healthMax[i] = stats.health[i];
        }

        WarriorAbility ability = stats.ability;
        ability.flying.Add();
        ability.pierce.Add();
        ability.spellImmunity.Add();

        return stats;
    }
}